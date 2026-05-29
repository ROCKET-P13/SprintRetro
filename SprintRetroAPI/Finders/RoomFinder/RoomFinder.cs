using Microsoft.EntityFrameworkCore;
using SprintRetroAPI.Data;
using SprintRetroAPI.Entities;
using SprintRetroAPI.Finders.RoomFinder.Interfaces;

namespace SprintRetroAPI.Finders.RoomFinder;

public class RoomFinder(AppDatabaseContext databaseContext) : IRoomFinder
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task<Room?> ById(Guid id)
	{
		return await _databaseContext.Rooms
			.AsNoTracking()
			.AsSplitQuery()
			.Where(room => room.Id == id)
			.Include(room => room.Participants)
			.Include(room => room.Columns)
				.ThenInclude(column => column.Comments)
					.ThenInclude(comment => comment.Votes)
						.ThenInclude(vote => vote.Participant)
			.FirstOrDefaultAsync();
	}
}