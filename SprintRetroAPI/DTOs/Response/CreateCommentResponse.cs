namespace SprintRetroAPI.DTOs.Response;

public class CreateCommentResponse
{
	public required Guid Id { get; set; }
	public required string Body { get; set; }
	public int VoteCount { get; set; }
	public required string CreatedBy { get; set; }
}