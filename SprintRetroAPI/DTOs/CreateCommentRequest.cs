namespace SprintRetroAPI.DTOs;

public class CreateCommentRequest
{
	public Guid RoomId { get; set; }
	public Guid ColumnId { get; set; }
	public Guid ParticipantId { get; set; }
	public string Body { get; set; } = string.Empty;
}