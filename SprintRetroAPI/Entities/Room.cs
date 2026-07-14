

using SprintRetroAPI.DTOs.Request;

namespace SprintRetroAPI.Entities;

public class Room
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTimeOffset CreatedAt { get; set; }
	public Guid CreatedBy { get; set; }

	public List<Column> Columns { get; private set; } = [];
	public List<Participant> Participants { get; private set; } = [];

	public void AddParticipant(string participantName)
	{
		var participantId = Guid.NewGuid();
		if (Participants.Count == 0)
		{
			CreatedBy = participantId;
		}

		Participants.Add(
			new Participant
			{
				Id = participantId,
				RoomId = Id,
				Name = participantName,
			}
		);
	}

	public Column AddColumn(string title, int? position = null)
	{
		var columnPosition = position ?? GetNextAvailableColumnPosition();

		if (Columns.Any(column => column.Position == columnPosition))
		{
			throw new InvalidOperationException("A column already exists at provided position");
		}

		var column = new Column
		{
			Id = Guid.NewGuid(),
			RoomId = Id,
			Title = title,
			Position = columnPosition,
		};

		Columns.Add(column);

		return column;
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

	public void UpdateColumns(List<UpdateColumnPositionsRequestColumn> columns)
	{
		foreach(var column in columns)
		{
			var roomColumn = Columns.FirstOrDefault(roomColumn => roomColumn.Id == column.Id);
			if (roomColumn is null)
			{
				throw new InvalidOperationException("Column does not exist in room");
			}

			if (column.Position > 0)
			{
				roomColumn.UpdatePosition(column.Position);
			}
		}
	}

	public void RemoveColumn(Guid columnId)
	{
		var column = Columns.FirstOrDefault(column => column.Id == columnId);
		if (column is null)
		{
			throw new InvalidOperationException("Column does not exist in room");
		}

		Columns.Remove(column);

		for (var i = 0; i < Columns.Count; i++)
		{
			Columns[i].UpdatePosition(i + 1);
		}

	}

	public void UpdateColumnTitle(Guid columnId, string title)
	{
		var column = Columns.FirstOrDefault(column => column.Id == columnId);
		if (column is null)
		{
			throw new InvalidOperationException("Column does not exist in room");
		}

		column.UpdateTitle(title);
	}

	public Comment MergeComments(Guid parentCommentId, Guid childCommentId)
	{
		var commentsById = Columns
			.SelectMany(column => column.Comments)
			.Where(comment => comment.Id == childCommentId || comment.Id == parentCommentId)
			.ToDictionary(comment => comment.Id);

		var parentComment = commentsById[parentCommentId];
		var childComment = commentsById[childCommentId];

		if (parentComment is null)
		{
			throw new InvalidOperationException("Parent comment does not exist in room");
		}

		if (childComment is null)
		{
			throw new InvalidOperationException("Child comment does not exist in room");
		}

		parentComment.AddChild(childComment);

		return parentComment;
	}
}