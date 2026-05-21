namespace SprintRetroAPI.Entities;

public class Participant
{
	public Guid Id { get; set; }
	public Guid RoomId { get; set; }
	public string ConnectionId { get; set; } = string.Empty;
	public required string Name { get; set; }
	
	public void UpdateName(string name)
	{
		ArgumentNullException.ThrowIfNull(name);
		Name = name;
	}
}