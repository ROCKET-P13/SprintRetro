namespace SprintRetroAPI.Services.RoomConnectionManager.Interfaces;

public interface IRoomConnectionManager
{
	Task AddToRoom(string roomId, string connectionId);
	Task RemoveFromRoom(string roomId, string connectionId);
	IReadOnlyCollection<string> GetConnections(string roomId);
}