namespace SprintRetroAPI.Entities;

public class Comment
{
	public required Guid Id { get; set; }
	public required Guid RoomId { get; set; }
	public Room Room { get; set; } = null!;
	public required Guid ColumnId { get; set; }
	public Column Column { get; set; } = null!;
	public required Guid ParticipantId { get; set; }
	public Participant Participant { get; set; } = null!;
	public required string Body { get; set; }
	public int VoteCount { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
}