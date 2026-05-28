namespace SprintRetroAPI.Entities;

public class Comment
{
	public Guid Id { get; set; }
	public Guid RoomId { get; set; }
	public Room Room { get; set; } = null!;
	public Guid ColumnId { get; set; }
	public Column Column { get; set; } = null!;
	public Guid ParticipantId { get; set; }
	public Participant Participant { get; set; } = null!;
	public string Body { get; set; } = string.Empty;
	public int VoteCount { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
}