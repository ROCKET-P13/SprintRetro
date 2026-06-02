using System.Net.WebSockets;

namespace SprintRetroAPI.Services.WebSockets.LocalWebSocketConnectionManager.Interfaces;

public interface ILocalWebSocketConnectionManager
{
	Task Add(string connectionId, WebSocket socket);
	Task Remove(string connectionId);
	Task Send(string connectionId, string payload);
	IReadOnlyDictionary<string, WebSocket> Connections { get; }
}