namespace SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;

public class ParticipantViewModel
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required bool IsRoomAdmin { get; set; }
}