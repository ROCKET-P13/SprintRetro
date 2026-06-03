using System.Collections.Concurrent;
using SprintRetroAPI.Services.RoomConnectionManager.Interfaces;
using SprintRetroAPI.Services.RoomConnectionManager.Parameters;

namespace SprintRetroAPI.Services.RoomConnectionManager;
public class RoomConnectionManager : IRoomConnectionManager
{
	private readonly ConcurrentDictionary<string, HashSet<string>> _rooms = new();
	private readonly ConcurrentDictionary<string, string> _participantIdByConnectionId = new();

	public Task AddToRoom(AddToRoomParameters parameters)
	{
		var room = _rooms.GetOrAdd(parameters.RoomId, _ => new HashSet<string>());

		lock (room)
		{
			_participantIdByConnectionId[parameters.ConnectionId] = parameters.ParticipantId;
			room.Add(parameters.ConnectionId);
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