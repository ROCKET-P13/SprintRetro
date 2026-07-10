namespace SprintRetroAPI.DTOs.Response;


public class UpdateColumnPositionsResponseColumn
{
	public required Guid Id { get; set; }
	public required string Title { get; set; }
	public required int Position { get; set; }
}

public class UpdateColumnPositionsResponse
{
	public List<UpdateColumnPositionsResponseColumn> Columns { get; set; } = [];
}