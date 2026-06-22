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
				.. room.Columns
				.OrderBy(column => column.Position)
				.Select(column => new ColumnViewModel
				{
					Id = column.Id,
					Title = column.Title,
					Position = column.Position,
					Comments = [
						.. column.Comments.Select(comment => new CommentViewModel
						{
							Id = comment.Id,
							Body = comment.Body,
							VoteCount = comment.Votes.Count,
							CreatedBy = room.Participants.First(participant => participant.Id == comment.ParticipantId).Name,
							Votes = [
								.. comment.Votes.Select(vote => new VoteViewModel
								{
									Id = vote.Id,
									ParticipantName = vote.Participant.Name,
								})
							]
						})
					]
				})
			],
			Participants = [
				.. room.Participants.Select(participant => new ParticipantViewModel
				{
					Id = participant.Id,
					Name = participant.Name,
					IsRoomAdmin = participant.IsRoomAdmin,
				})
			]
		};
	}
}
