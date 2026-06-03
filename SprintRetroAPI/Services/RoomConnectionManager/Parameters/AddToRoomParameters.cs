namespace SprintRetroAPI.Services.RoomConnectionManager.Parameters;

public class AddToRoomParameters
{
	public string RoomId { get; set; } = default!;
	public string ConnectionId { get; set; } = default!;
	public string ParticipantId { get; set; } = default!;
}