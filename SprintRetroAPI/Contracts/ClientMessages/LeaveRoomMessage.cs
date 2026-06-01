namespace SprintRetroAPI.Contracts.ClientMessages;

public class LeaveRoomMessage
{
	public string Type = "LEAVE_ROOM";
	public Guid RoomId { get; set; }
	public Guid ParticipantId { get; set; }
}