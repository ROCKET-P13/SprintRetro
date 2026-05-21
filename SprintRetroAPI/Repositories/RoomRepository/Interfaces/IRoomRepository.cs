using SprintRetroAPI.Entities;

namespace SprintRetroAPI.Repositories.RoomRepository.Interfaces;

public interface IRoomRepository
{
	Task<Room?> FindById(Guid id);
	void Upsert(Room room);
}