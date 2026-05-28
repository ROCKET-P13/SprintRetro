namespace SprintRetroAPI.Entities;

public class Participant
{
	public Guid Id { get; set; }
	public Guid RoomId { get; set; }
	public Room Room { get; set; } = null!;
	public string ConnectionId { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public List<Comment> Comments { get; private set; } = [];
	
	public void UpdateName(string name)
	{
		ArgumentNullException.ThrowIfNull(name);
		Name = name;
	}
}