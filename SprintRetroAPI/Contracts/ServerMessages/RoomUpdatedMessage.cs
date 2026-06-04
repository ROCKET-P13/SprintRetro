using SprintRetroAPI.Contracts.ServerMessages.Interfaces;
using SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;

namespace SprintRetroAPI.Contracts.ServerMessages;

public class RoomUpdatedMessage : IServerMessage
{
	public string Type => "ROOM_UPDATED";
	public RoomViewModel Payload { get; set; } = null!;
}