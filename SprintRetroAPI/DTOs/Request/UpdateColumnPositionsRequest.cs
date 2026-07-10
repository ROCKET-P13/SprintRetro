namespace SprintRetroAPI.DTOs.Request;

public class UpdateColumnPositionsRequestColumn
{
	public required Guid Id { get; set; }
	public int Position { get; set; }
}

public class UpdateColumnPositionsRequest
{
	public List<UpdateColumnPositionsRequestColumn> Columns { get; set; } = [];
}