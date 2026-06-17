namespace SprintRetroAPI.DTOs.Request;

public class VoteCommentRequest
{
	public required Guid ParticipantId { get; set; }
	public required Guid CommentId { get; set; }
}