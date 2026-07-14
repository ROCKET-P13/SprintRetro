namespace SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;

public class ChildCommentViewModel
{
	public required Guid Id { get; set; }
	public required string Body { get; set; }
	public required string CreatedBy { get; set; }
}