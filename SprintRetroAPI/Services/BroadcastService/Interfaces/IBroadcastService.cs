using SprintRetroAPI.Entities;

namespace SprintRetroAPI.Services.BroadcastService.Interfaces;

public interface IBroadcastService
{
	Task RoomUpdated(Room room);
}