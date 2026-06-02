using System.Collections.Concurrent;
using SprintRetroAPI.Services.RoomConnectionManager.Interfaces;

namespace SprintRetroAPI.Services.RoomConnectionManager;
public class RoomConnectionManager : IRoomConnectionManager
{
	private readonly ConcurrentDictionary<string, HashSet<string>> _rooms = new();

	public Task AddToRoom(string roomId, string connectionId)
	{
		var room = _rooms.GetOrAdd(roomId, _ => new HashSet<string>());

		lock (room)
		{
			room.Add(connectionId);
		}

		return Task.CompletedTask;
	}

	public Task RemoveFromRoom(string roomId, string connectionId)
	{
		if (_rooms.TryGetValue(roomId, out var room))
		{
			lock (room)
			{
				room.Remove(connectionId);
			}
		}

		return Task.CompletedTask;
	}

	public IReadOnlyCollection<string> GetConnections(string roomId)
	{
		if (!_rooms.TryGetValue(roomId, out var room))
			return [];

		lock (room)
		{
			return room.ToList();
		}
	}
}