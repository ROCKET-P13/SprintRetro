namespace SprintRetroAPI.DTOs.Request;



public class CreateRoomRequest
{

	public required string Name { get; set; } 
	public List<CreateColumnRequest> Columns { get; set; } = [];
	public string? ParticipantName { get; set; }
}