namespace SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;

public class CommentViewModel
{
	public required Guid Id { get; set; }
	public required string Body { get; set; }
	public int VoteCount { get; set; }
	public Guid ParticipantId { get; set; }
}