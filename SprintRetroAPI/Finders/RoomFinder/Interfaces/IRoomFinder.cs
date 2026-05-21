using SprintRetroAPI.Entities;

namespace SprintRetroAPI.Finders.RoomFinder.Interfaces;

public interface IRoomFinder
{
	Task<Room?> ById(Guid roomId);
}