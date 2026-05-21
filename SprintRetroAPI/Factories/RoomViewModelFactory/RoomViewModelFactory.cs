using SprintRetroAPI.Entities;
using SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;
using SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;

namespace SprintRetroAPI.Factories.RoomViewModelFactory;

public class RoomViewModelFactory : IRoomViewModelFactory
{
	public RoomViewModel FromRoom(Room room)
	{
		return new RoomViewModel
		{
			Id = room.Id,
			Name = room.Name,
			Columns = [
				.. room.Columns.Select(column => new ColumnViewModel
				{
					Id = column.Id,
					Title = column.Title,
					Position = column.Position,
					Comments = [
						.. column.Comments.Select(comment => new CommentViewModel
						{
							Id = comment.Id,
							Body = comment.Body,
							VoteCount = comment.VoteCount,
							ParticipantId = comment.ParticipantId
						})
					]
				})
			],
			Participants = [
				.. room.Participants.Select(participant => new ParticipantViewModel
				{
					Id = participant.Id,
					Name = participant.Name
				})
			]
		};
	}
}
