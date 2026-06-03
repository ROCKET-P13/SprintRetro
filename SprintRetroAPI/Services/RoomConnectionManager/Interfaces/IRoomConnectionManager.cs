using SprintRetroAPI.Services.RoomConnectionManager.Parameters;

namespace SprintRetroAPI.Services.RoomConnectionManager.Interfaces;

public interface IRoomConnectionManager
{
	Task AddToRoom(AddToRoomParameters parameters);
	Task RemoveFromRoom(string roomId, string connectionId);
	IReadOnlyCollection<string> GetConnections(string roomId);
}