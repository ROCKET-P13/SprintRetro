namespace SprintRetroAPI.DTOs.Response;

public class VoteCommentResponse
{
	public required Guid Id { get; set; }
	public required Guid ColumnId { get; set; }
	public required Guid CommentId { get; set; }
	public required string ParticipantName { get; set; }
}