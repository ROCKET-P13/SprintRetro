using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using SprintRetroAPI.Services.WebSockets.WebSocketConnectionManager.Interfaces;

namespace SprintRetroAPI.Services.WebSockets.WebSocketConnectionManager;
public class WebSocketConnectionManager : IWebSocketConnectionManager
{
	private readonly ConcurrentDictionary<string, WebSocket> _connections = new();

	public IReadOnlyDictionary<string, WebSocket> Connections => _connections;

	public Task Add(string connectionId, WebSocket webSocket)
	{
		_connections[connectionId] = webSocket;
		return Task.CompletedTask;
	}

	public Task Remove(string connectionId)
	{
		_connections.TryRemove(connectionId, out _);
		return Task.CompletedTask;
	}

	public async Task Send(string connectionId, string payload)
	{
		if (!_connections.TryGetValue(connectionId, out var socket))
			return;

		if (socket.State != WebSocketState.Open)
		{
			_connections.TryRemove(connectionId, out _);
			return;
		}

		var buffer = Encoding.UTF8.GetBytes(payload);

		await socket.SendAsync(
			buffer,
			WebSocketMessageType.Text,
			WebSocketMessageFlags.EndOfMessage,
			CancellationToken.None
		);
	}
}