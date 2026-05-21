namespace SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;

public class ColumnViewModel
{
	public required Guid Id { get; set; }
	public required string Title { get; set; }
	public int Position { get; set; }
	public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>(); 
}