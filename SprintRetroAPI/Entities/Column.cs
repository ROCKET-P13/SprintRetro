namespace SprintRetroAPI.Entities;

public class Column
{
	public required Guid Id { get; set; }
	public required Guid RoomId { get; set; }
	public Room Room { get; set; } = null!;
	public required string Title { get; set; }
	public int Position { get; set; }
	public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
	
	public void AddComment(Comment comment)
	{
		Comments.Add(comment);
	}
}