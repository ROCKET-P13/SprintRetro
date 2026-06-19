using System.Text.Json;
using SprintRetroAPI.Serialization;
using SprintRetroAPI.Services.WebSockets.WebSocketConnectionManager.Interfaces;
using SprintRetroAPI.Services.WebSockets.WebSocketPublisher.Interfaces;

namespace SprintRetroAPI.Services.WebSockets.WebSocketPublisher;

public class WebSocketPublisher(IWebSocketConnectionManager WebSocketConnectionManager) : IWebSocketPublisher
{
	private readonly IWebSocketConnectionManager _webSocketConnectionManager = WebSocketConnectionManager;

	public async Task PublishToConnection(string connectionId, object payload)
	{
		await _webSocketConnectionManager.Send(
			connectionId,
			JsonSerializer.Serialize(payload, AppJsonSerializerOptions.ApplicationDefault)
		);
	}
}