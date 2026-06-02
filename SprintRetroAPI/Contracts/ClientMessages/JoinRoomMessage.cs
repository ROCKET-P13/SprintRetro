using SprintRetroAPI.Contracts.ClientMessages.Interfaces;

namespace SprintRetroAPI.Contracts.ClientMessages;

public class JoinRoomMessage : IClientMessage
{
	public string Type => "JOIN_ROOM";
	public Guid RoomId { get; set; }
	public Guid ParticipantId { get; set; }
}