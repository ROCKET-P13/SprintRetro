namespace SprintRetroAPI.Entities;

public class Vote
{
    public Guid Id { get; set; }

    public Guid CommentId { get; set; }
    public Comment Comment { get; set; } = null!;
    public Guid ParticipantId { get; set; }
    public Participant Participant { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}