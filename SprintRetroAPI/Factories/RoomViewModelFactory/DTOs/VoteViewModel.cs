namespace SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;

public class VoteViewModel
{
	public Guid Id { get; set; }
	public Guid ParticipantId { get; set; }
	public string ParticipantName { get; set; } = string.Empty;
}