using Microsoft.EntityFrameworkCore;
using SprintRetroAPI.Data;
using SprintRetroAPI.Entities;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;

namespace SprintRetroAPI.Repositories.RoomRepository;

public class RoomRepository(AppDatabaseContext databaseContext) : IRoomRepository
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<Room?> FindById(Guid id)
	{
		ArgumentNullException.ThrowIfNull(id);

		return await _databaseContext.Rooms
			.Where(room => room.Id == id)
			.Include(room => room.Participants)
			.Include(room => room.Columns)
				.ThenInclude(column => column.Comments)
				.ThenInclude(comment => comment.Votes)
			.FirstOrDefaultAsync();
	}

	public void Upsert(Room room)
	{
		ArgumentNullException.ThrowIfNull(room);
		_databaseContext.Rooms.Add(room);
	}
}