namespace SprintRetroAPI.DTOs.Request;

public class CreateColumnRequest
{
	public Guid RoomId { get; set; }
	public string Title { get; set; } = string.Empty;
	public int? Position { get; set; }
}