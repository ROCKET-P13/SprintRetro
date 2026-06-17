namespace SprintRetroAPI.DTOs.Request;

public class CreateCommentRequest
{
	public Guid ColumnId { get; set; }
	public Guid ParticipantId { get; set; }
	public string Body { get; set; } = string.Empty;
}