using System.Text.Json;
using SprintRetroAPI.Serialization;

namespace SprintRetroAPI.Services.WebSockets;

public class WebSocketMessageRouter
{
	private readonly Dictionary<string, Func<string, JsonElement, Task<object?>>> _handlers = new();

	public void Register(string type, Func<string, JsonElement, Task<object?>> handler)
	{
		_handlers[type] = handler;
	}

	public async Task<object?> Route(string connectionId, string rawMessage)
	{
		Console.WriteLine($"RAW MESSAGE: {rawMessage}");

		var envelope = JsonSerializer.Deserialize<ClientMessageEnvelope>(rawMessage, AppJsonSerializerOptions.ApplicationDefault);

		if (envelope is null)
		{

			throw new Exception("Failed to deserialize message");
		}

		if (string.IsNullOrWhiteSpace(envelope.Type))
		{
			throw new Exception("Message type is empty");
		}

		Console.WriteLine($"Handler count: {_handlers.Count}");

		_handlers.TryGetValue(envelope.Type, out var handler);
		if (handler is null)
		{
			throw new Exception($"No handler found for '{envelope.Type}'");
		}

		return await handler(connectionId, envelope.Payload);
	}
}