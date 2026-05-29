namespace SprintRetroAPI.DTOs;

public class VoteCommentRequest
{
	public required Guid ParticipantId { get; set; }
	public required Guid CommentId { get; set; }
}