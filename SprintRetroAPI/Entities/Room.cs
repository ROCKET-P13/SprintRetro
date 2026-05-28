
using SprintRetroAPI.DTOs;

namespace SprintRetroAPI.Entities;

public class Room
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTimeOffset CreatedAt { get; set; }

	public List<Column> Columns { get; private set; } = [];
	public List<Participant> Participants { get; private set; } = [];

	public void AddParticipant(Participant participant)
	{
		Participants.Add(participant);
	}

	public void AddColumn(string title, int position)
	{
		if (Columns.FirstOrDefault(c => c.Position == position) is not null)
		{
			throw new InvalidOperationException("A column already exists at provided position");
		}
		
		Columns.Add(
			new Column
			{
				Id = Guid.NewGuid(),
				RoomId = Id,
				Title = title,
				Position = position,
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