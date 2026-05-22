
using SprintRetroAPI.DTOs;

namespace SprintRetroAPI.Entities;

public class Room
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required DateTimeOffset CreatedAt { get; set; }

	public ICollection<Column> Columns { get; private set; } = new List<Column>();
	public ICollection<Participant> Participants { get; private set; } = new List<Participant>();

	public void AddParticipant(Participant participant)
	{
		Participants.Add(participant);
	}

	public void AddColumn(CreateColumnRequest dto)
	{
		if (Columns.FirstOrDefault(column => column.Position == dto.Position) is not null)
		{
			throw new InvalidOperationException("A column already exists at provided position");
		}
		
		Columns.Add(
			new Column
			{
				Id = Guid.NewGuid(),
				RoomId = Id,
				Title = dto.Title,
				Position = dto.Position,
			}
		);
	}

	public void AddComment(CreateCommentRequest dto)
	{
		var column = Columns.FirstOrDefault(column => column.Id == dto.ColumnId);
		if (column is null)
		{
			throw new InvalidOperationException("Column does not exist in room");
		}

		column.AddComment(
			new Comment
			{
				Id = Guid.NewGuid(),
				RoomId = Id,
				ColumnId = column.Id,
				ParticipantId = dto.ParticipantId,
				Body = dto.Body,
				VoteCount = 0,
				CreatedAt = DateTimeOffset.UtcNow
			}
		);
	}
}