using System.Text.Json;

namespace SprintRetroAPI.Services.WebSockets;

public class ServerMessageEnvelope
{
	public string Type { get; set; } = default!;
	public JsonElement Payload { get; set; }
}