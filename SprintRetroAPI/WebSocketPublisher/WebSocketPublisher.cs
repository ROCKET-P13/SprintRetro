using System.Text.Json;
using SprintRetroAPI.Serialization;
using SprintRetroAPI.Services.WebSockets.WebSocketConnectionManager.Interfaces;
using SprintRetroAPI.WebSocketPublisher.Interfaces;

namespace SprintRetroAPI.WebSocketPublisher;

public class WebSocketPublisher(IWebSocketConnectionManager WebSocketConnectionManager) : IWebSocketPublisher
{
	private readonly IWebSocketConnectionManager _WebSocketConnectionManager = WebSocketConnectionManager;

	public async Task PublishToConnection(string connectionId, object payload)
	{
		await _WebSocketConnectionManager.Send(connectionId, JsonSerializer.Serialize(payload, AppJsonSerializerOptions.ApplicationDefault));
	}
}