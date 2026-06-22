namespace SprintRetroAPI.DTOs.Request;



public class CreateRoomRequest
{

	public required string Name { get; set; } 
	public List<CreateColumnRequest> Columns { get; set; } = [];
	public required string ParticipantName { get; set; }
}