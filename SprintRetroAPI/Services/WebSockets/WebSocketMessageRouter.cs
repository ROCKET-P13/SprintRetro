using System.Text.Json;
using SprintRetroAPI.Serialization;

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
		Console.WriteLine($"RAW MESSAGE: {rawMessage}");

		var envelope = JsonSerializer.Deserialize<ClientMessageEnvelope>(rawMessage, AppJsonSerializerOptions.ApplicationDefault);

		if (envelope is null)
		{
			Console.WriteLine("Failed to deserialize message");
			return;
		}

		if (string.IsNullOrWhiteSpace(envelope.Type))
		{
			Console.WriteLine("Message type is empty");
			return;
		}

		Console.WriteLine($"Handler count: {_handlers.Count}");

		_handlers.TryGetValue(envelope.Type, out var handler);
		if (handler is null)
		{
			Console.WriteLine($"No handler found for '{envelope.Type}'");
			return;
		}

		await handler(connectionId, envelope.Payload);
	}
}