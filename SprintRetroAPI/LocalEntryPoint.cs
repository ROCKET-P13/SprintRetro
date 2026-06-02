using System.Net.WebSockets;
using System.Text;
using SprintRetroAPI.Services.WebSockets.LocalWebSocketConnectionManager.Interfaces;
using SprintRetroAPI.Services.WebSockets;
using SprintRetroAPI.Services.WebSockets.Handlers;

namespace SprintRetroAPI;

public static class LocalEntryPoint
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddApplication(
			builder.Configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Missing connection string")
		);

		var app = builder.Build();
		var router = app.Services.GetRequiredService<WebSocketMessageRouter>();
		app.UseWebSockets();

		app.Map("/ws", async context =>
		{
			// ---------------------------
			// Resolve required services
			// ---------------------------
			var lifetime = context.RequestServices.GetRequiredService<IHostApplicationLifetime>();
			var connectionManager = context.RequestServices.GetRequiredService<ILocalWebSocketConnectionManager>();

			// ---------------------------
			// Graceful shutdown token
			// ---------------------------
			using var cts = CancellationTokenSource.CreateLinkedTokenSource(
				lifetime.ApplicationStopping
			);

			// ---------------------------
			// Accept WebSocket
			// ---------------------------
			var socket = await context.WebSockets.AcceptWebSocketAsync();

			var connectionId = Guid.NewGuid().ToString();

			await connectionManager.Add(connectionId, socket);

			Console.WriteLine($"WS CONNECTED: {connectionId}");

			var buffer = new byte[1024];

			try
			{
				while (!cts.Token.IsCancellationRequested &&
					   socket.State == WebSocketState.Open)
				{
					// Receive message
					var result = await socket.ReceiveAsync(buffer, cts.Token);

					// Handle close frame
					if (result.MessageType == WebSocketMessageType.Close)
						break;

					// Decode message
					var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);

					Console.WriteLine($"[{connectionId}] {msg}");

					// Route message to handlers
					await router.Route(connectionId, msg);
				}
			}
			catch (OperationCanceledException)
			{
				// triggered by app shutdown
			}
			finally
			{
				// ---------------------------
				// Cleanup connection
				// ---------------------------
				await connectionManager.Remove(connectionId);

				if (socket.State == WebSocketState.Open)
				{
					await socket.CloseAsync(
						WebSocketCloseStatus.NormalClosure,
						"Server shutting down",
						CancellationToken.None
					);
				}

				Console.WriteLine($"WS DISCONNECTED: {connectionId}");
			}
		});

		app.MapControllers();

		app.Run();
	}
}