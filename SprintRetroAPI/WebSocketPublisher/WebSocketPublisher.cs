using System.Text.Json;
using SprintRetroAPI.Serialization;
using SprintRetroAPI.Services.WebSockets.LocalWebSocketConnectionManager.Interfaces;
using SprintRetroAPI.WebSocketPublisher.Interfaces;

namespace SprintRetroAPI.WebSocketPublisher;

public class WebSocketPublisher(ILocalWebSocketConnectionManager localWebSocketConnectionManager) : IWebSocketPublisher
{
	private readonly ILocalWebSocketConnectionManager _localWebSocketConnectionManager = localWebSocketConnectionManager;

	public async Task PublishToConnection(string connectionId, object payload)
	{
		await _localWebSocketConnectionManager.Send(connectionId, JsonSerializer.Serialize(payload, AppJsonSerializerOptions.ApplicationDefault));
	}
}