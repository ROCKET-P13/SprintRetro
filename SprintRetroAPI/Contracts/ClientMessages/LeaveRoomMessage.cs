using SprintRetroAPI.Contracts.ClientMessages.Interfaces;

namespace SprintRetroAPI.Contracts.ClientMessages;

public class LeaveRoomMessage : IClientMessage
{
	public string Type => "LEAVE_ROOM";
	public Guid RoomId { get; set; }
	public Guid ParticipantId { get; set; }
}