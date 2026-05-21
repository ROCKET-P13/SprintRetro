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
			.FirstOrDefaultAsync();
	}

	public void Upsert(Room room)
	{
		ArgumentNullException.ThrowIfNull(room);

		var roomEntity = _databaseContext.Entry(room);
		if (roomEntity.State == EntityState.Detached)
		{
			_databaseContext.Rooms.Add(room);
			return;
		}

		foreach (var participant in room.Participants)
		{
			if (_databaseContext.Entry(participant).State == EntityState.Modified)
			{
				_databaseContext.Participants.Add(participant);
			}
		}

		foreach (var column in room.Columns)
		{
			if (_databaseContext.Entry(column).State == EntityState.Modified)
			{
				_databaseContext.Columns.Add(column);
			}

			foreach (var comment in column.Comments)
			{
				if (_databaseContext.Entry(comment).State == EntityState.Modified)
				{
					_databaseContext.Comments.Add(comment);
				}
			}
		}
	}
}