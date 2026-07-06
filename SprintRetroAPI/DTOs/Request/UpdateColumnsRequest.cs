namespace SprintRetroAPI.DTOs.Request;

public class UpdateColumnsRequestColumn
{
	public required Guid Id { get; set; }
	public int Position { get; set; }
}

public class UpdateColumnsRequest
{
	public List<UpdateColumnsRequestColumn> Columns { get; set; } = [];
}