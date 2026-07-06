using SprintRetroAPI.DTOs.Request;

namespace SprintRetroAPI.Entities;

public class Column
{
	public Guid Id { get; set; }
	public Guid RoomId { get; set; }
	public Room Room { get; set; } = null!;
	public string Title { get; set; } = string.Empty;
	public int Position { get; set; }
	public List<Comment> Comments { get; private set; } = [];
	
	public Comment AddComment(CreateCommentRequest dto)
	{
		var comment = new Comment
		{
			Id = Guid.NewGuid(),
			RoomId = RoomId,
			ColumnId = Id,
			ParticipantId = dto.ParticipantId,
			Body = dto.Body,
			CreatedAt = DateTimeOffset.UtcNow
		};
		Comments.Add(comment);
		return comment;
	}

	public void UpdatePosition(int position)
	{
		Position = position;
	}
}