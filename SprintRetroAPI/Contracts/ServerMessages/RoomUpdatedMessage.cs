using SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;

namespace SprintRetroAPI.Contracts.ServerMessages;

public class RoomUpdatedMessage
{
	public string Type = "ROOM_UPDATED";
	public RoomViewModel Room { get; set; } = null!;
}