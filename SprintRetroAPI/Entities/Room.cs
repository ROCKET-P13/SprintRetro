

using SprintRetroAPI.DTOs.Request;

namespace SprintRetroAPI.Entities;

public class Room
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTimeOffset CreatedAt { get; set; }

	public List<Column> Columns { get; private set; } = [];
	public List<Participant> Participants { get; private set; } = [];

	public void AddParticipant(string participantName)
	{
		Participants.Add(
			new Participant
			{
				Id = Guid.NewGuid(),
				RoomId = Id,
				Name = participantName,
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

	public Comment AddComment(CreateCommentRequest dto)
	{
		var column = Columns.FirstOrDefault(column => column.Id == dto.ColumnId);
		if (column is null)
		{
			throw new InvalidOperationException("Column does not exist in room");
		}

		return column.AddComment(dto);
	}

	public Vote AddVote(VoteCommentRequest dto)
	{
		var comment = Columns.SelectMany(column => column.Comments)
			.FirstOrDefault(comment => comment.Id == dto.CommentId); 

		if (comment is null)
		{
			throw new InvalidOperationException("Comment does not exist in room");
		}

		return comment.AddVote(dto);
	}

	public void RemoveVote(Guid voteId)
	{
		var comment = Columns.SelectMany(column => column.Comments)
			.First(comment => comment.Votes.Any(vote => vote.Id == voteId));

		if (comment is null)
		{
			return;
		}

		comment.RemoveVote(voteId);
	}
}