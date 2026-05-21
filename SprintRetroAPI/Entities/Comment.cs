namespace SprintRetroAPI.Entities;

public class Comment
{
	public required Guid Id { get; set; }
	public required Guid RoomId { get; set; }
	public required Guid ColumnId { get; set; }
	public required Guid ParticipantId { get; set; }
	public required string Body { get; set; }
}