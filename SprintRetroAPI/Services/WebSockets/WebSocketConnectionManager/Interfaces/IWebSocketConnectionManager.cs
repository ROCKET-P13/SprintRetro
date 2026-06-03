using System.Net.WebSockets;

namespace SprintRetroAPI.Services.WebSockets.WebSocketConnectionManager.Interfaces;

public interface IWebSocketConnectionManager
{
	Task Add(string connectionId, WebSocket socket);
	Task Remove(string connectionId);
	Task Send(string connectionId, string payload);
	IReadOnlyDictionary<string, WebSocket> Connections { get; }
}