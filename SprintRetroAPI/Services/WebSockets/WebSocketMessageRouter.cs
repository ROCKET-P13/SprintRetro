using System.Text.Json;

namespace SprintRetroAPI.Services.WebSockets;

public class WebSocketMessageRouter
{
	private readonly Dictionary<string, Func<string, JsonElement, Task>> _handlers = new();

	public void Register(string type, Func<string, JsonElement, Task> handler)
	{
		_handlers[type] = handler;
	}

	public async Task Route(string connectionId, string rawMessage)
	{
		var envelope = JsonSerializer.Deserialize<ClientMessageEnvelope>(rawMessage);

		if (envelope is null)
			return;

		if (_handlers.TryGetValue(envelope.Type, out var handler))
		{
			await handler(connectionId, envelope.Payload);
		}
	}
}