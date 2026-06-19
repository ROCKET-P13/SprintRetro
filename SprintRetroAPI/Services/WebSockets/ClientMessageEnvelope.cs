using System.Text.Json;

namespace SprintRetroAPI.Services.WebSockets;

public class ClientMessageEnvelope
{
	public string Type { get; set; } = default!;
	public required string RequestId { get; set; }
	public JsonElement Payload { get; set; }
}