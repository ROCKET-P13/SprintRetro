namespace SprintRetroAPI.Entities;

public class Participant
{
	public Guid Id { get; set; }
	public Guid RoomId { get; set; }
	public Room Room { get; set; } = null!;
	public string ConnectionId { get; set; } = string.Empty;
	public required string Name { get; set; }
	public ICollection<Comment> Comments { get; set; } = new List<Comment>();
	
	public void UpdateName(string name)
	{
		ArgumentNullException.ThrowIfNull(name);
		Name = name;
	}
}