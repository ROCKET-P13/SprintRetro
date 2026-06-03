namespace SprintRetroAPI.Services.WebSockets.WebSocketPublisher.Interfaces;

public interface IWebSocketPublisher
{
	Task PublishToConnection(string connectionId, object payload);
}