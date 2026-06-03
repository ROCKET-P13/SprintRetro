using System.Net.WebSockets;
using System.Text;
using SprintRetroAPI.Services.WebSockets.WebSocketConnectionManager.Interfaces;
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
		var joinRoomHandler = app.Services.GetRequiredService<JoinRoomHandler>();
		router.Register("JOIN_ROOM", joinRoomHandler.Handle);
        app.UseWebSockets();

        app.Map("/ws", async context =>
        {
            var lifetime = context.RequestServices.GetRequiredService<IHostApplicationLifetime>();
            var connectionManager = context.RequestServices.GetRequiredService<IWebSocketConnectionManager>();

            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                lifetime.ApplicationStopping
            );

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            var connectionId = Guid.NewGuid().ToString();

            await connectionManager.Add(connectionId, socket);
            Console.WriteLine($"WS Connected: {connectionId}");

            var buffer = new byte[1024];

            try
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested &&
                       socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(buffer, cancellationTokenSource.Token);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    Console.WriteLine($"[{connectionId}] {message}");

                    await router.Route(connectionId, message);
                }
            }
            catch (OperationCanceledException)
            {
                // triggered by app shutdown
            }
            finally
            {
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