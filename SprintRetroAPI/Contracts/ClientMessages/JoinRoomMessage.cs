namespace SprintRetroAPI.Contracts.ClientMessages;

public class JoinRoomMessage
{
	public string Type = "JOIN_ROOM";
	public Guid RoomId { get; set; }
	public Guid ParticipantId { get; set; }
}