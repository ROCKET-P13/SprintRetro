
namespace SprintRetroAPI.Entities;

public class Room
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required DateTimeOffset CreatedAt { get; set; }

	public ICollection<Column> Columns { get; private set; } = new List<Column>();
	public ICollection<Participant> Participants { get; private set; } = new List<Participant>();

	public void AddParticipant(Participant participant)
	{
		Participants.Add(participant);
	}

	public void AddColumn(Column column)
	{
		Columns.Add(column);
	}
}