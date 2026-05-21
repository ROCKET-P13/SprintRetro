namespace SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;

public class RoomViewModel
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public List<ColumnViewModel> Columns { get; set; } = new List<ColumnViewModel>();
	public List<ParticipantViewModel> Participants { get; set; } = new List<ParticipantViewModel>();

}