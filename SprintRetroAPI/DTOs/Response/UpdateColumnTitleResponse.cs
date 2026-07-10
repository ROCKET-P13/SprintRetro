namespace SprintRetroAPI.DTOs.Response;

public class UpdateColumnTitleResponse
{
	public Guid Id { get; set; }
	public required string Title { get; set; }
}