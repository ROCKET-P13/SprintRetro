using SprintRetroAPI.DTOs;

namespace SprintRetroAPI.Entities;

public class Column
{
	public Guid Id { get; set; }
	public Guid RoomId { get; set; }
	public Room Room { get; set; } = null!;
	public string Title { get; set; } = string.Empty;
	public int Position { get; set; }
	public List<Comment> Comments { get; private set; } = [];
	
	public void AddComment(CreateCommentRequest dto)
	{
		Comments.Add(
		new Comment
			{
				Id = Guid.NewGuid(),
				RoomId = RoomId,
				ColumnId = Id,
				ParticipantId = dto.ParticipantId,
				Body = dto.Body,
				CreatedAt = DateTimeOffset.UtcNow
			}
		);
	}
}