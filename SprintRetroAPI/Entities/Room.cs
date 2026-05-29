
using SprintRetroAPI.DTOs;

namespace SprintRetroAPI.Entities;

public class Room
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTimeOffset CreatedAt { get; set; }

	public List<Column> Columns { get; private set; } = [];
	public List<Participant> Participants { get; private set; } = [];

	public void AddParticipant(CreateParticipantRequest dto)
	{
		Participants.Add(
			new Participant
			{
				Id = Guid.NewGuid(),
				RoomId = Id,
				Name = dto.Name,
			}
		);
	}

	public void AddColumn(string title, int? position = null)
	{
		var columnPosition = position ?? GetNextAvailableColumnPosition();

		if (Columns.Any(column => column.Position == columnPosition))
		{
			throw new InvalidOperationException("A column already exists at provided position");
		}

		Columns.Add(
			new Column
			{
				Id = Guid.NewGuid(),
				RoomId = Id,
				Title = title,
				Position = columnPosition,
			}
		);
	}

	private int GetNextAvailableColumnPosition()
	{
		var position = 1;

		while (Columns.Any(column => column.Position == position))
		{
			position++;
		}

		return position;
	}

	public void AddComment(CreateCommentRequest dto)
	{
		var column = Columns.FirstOrDefault(column => column.Id == dto.ColumnId);
		if (column is null)
		{
			throw new InvalidOperationException("Column does not exist in room");
		}

		column.AddComment(dto);
	}

	public void AddVote(VoteCommentRequest dto)
	{
		var comment = Columns.SelectMany(column => column.Comments)
			.FirstOrDefault(comment => comment.Id == dto.CommentId); 

		if (comment is null)
		{
			throw new InvalidOperationException("Comment does not exist in room");
		}

		comment.AddVote(dto);
	}

	public void RemoveVote(VoteCommentRequest dto)
	{
		var comment = Columns.SelectMany(column => column.Comments)
			.FirstOrDefault(comment => comment.Id == dto.CommentId); 

		if (comment is null)
		{
			return;
		}

		comment.RemoveVote(dto);
	}
}