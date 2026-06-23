namespace SprintRetroAPI.DTOs.Response;

public class CreateColumnResponse
{
	public required Guid Id { get; set; }
	public required string Title { get; set; }
	public int Position { get; set; }
}