using SprintRetroAPI.DTOs.Request;

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
    public DateTimeOffset CreatedAt { get; set; }
    public Guid? ParentCommentId { get; set; }
    public Comment ParentComment { get; set; } = null!;
    public List<Vote> Votes { get; private set; } = [];
    public List<Comment> ChildComments { get; private set; } = [];

    public Vote AddVote(VoteCommentRequest dto)
    {
        if (Votes.Any(vote => vote.ParticipantId == dto.ParticipantId))
        {
            throw new InvalidOperationException("Participant already voted on this comment");
        }

        var vote = new Vote
        {
            Id = Guid.NewGuid(),
            CommentId = Id,
            ParticipantId = dto.ParticipantId,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        Votes.Add(vote);
        return vote;
    }

    public void RemoveVote(Guid voteId)
    {
        var vote = Votes.FirstOrDefault(vote => vote.Id == voteId);

        if (vote is null)
        {
            return;
        }

        Votes.Remove(vote);
    }

    public void AddChild(Comment comment)
    {
        if (comment.ParentCommentId != Guid.Empty)
        {
            throw new InvalidOperationException("Comment already has parent");
        }

        ChildComments.Add(comment);
    }
}