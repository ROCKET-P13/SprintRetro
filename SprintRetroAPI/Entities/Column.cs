namespace SprintRetroAPI.Entities;

public class Column
{
	public Guid Id { get; set; }
	public Guid RoomId { get; set; }
	public Room Room { get; set; } = null!;
	public string Title { get; set; } = string.Empty;
	public int Position { get; set; }
	public List<Comment> Comments { get; private set; } = [];
	
	public void AddComment(Comment comment)
	{
		Comments.Add(comment);
	}
}