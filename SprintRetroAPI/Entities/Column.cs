namespace SprintRetroAPI.Entities;

public class Column
{
	public required Guid Id { get; set; }
	public required Guid RoomId { get; set; }
	public required string Name { get; set; }
	public int? SortOrder { get; set; }
}