namespace SprintRetroAPI.DTOs.Response;


public class UpdateColumnsResponseColumn
{
	public required Guid Id { get; set; }
	public required string Title { get; set; }
	public required int Position { get; set; }
}

public class UpdateColumnsResponse
{
	public List<UpdateColumnsResponseColumn> Columns { get; set; } = [];
}