namespace SprintRetroAPI.Services.WebSockets;

public class ServerMessageEnvelope
{
	public required string Type { get; set; }
	public string? RequestId { get; set; }
	public required bool Success { get; set; }
	public object? Payload { get; set; }
	public string? Error { get; set; }
}