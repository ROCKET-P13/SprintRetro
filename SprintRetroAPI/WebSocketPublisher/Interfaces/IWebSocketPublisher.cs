namespace SprintRetroAPI.WebSocketPublisher.Interfaces;

public interface IWebSocketPublisher
{
	Task PublishToConnection(string connectionId, object payload);
}