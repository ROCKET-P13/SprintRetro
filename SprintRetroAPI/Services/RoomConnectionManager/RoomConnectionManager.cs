using System.Collections.Concurrent;
using SprintRetroAPI.Services.RoomConnectionManager.Interfaces;
using SprintRetroAPI.Services.RoomConnectionManager.Parameters;

namespace SprintRetroAPI.Services.RoomConnectionManager;
public class RoomConnectionManager : IRoomConnectionManager
{
	private readonly ConcurrentDictionary<string, HashSet<string>> _connectionsByRoomId = new();
	private readonly ConcurrentDictionary<string, string> _participantIdByConnectionId = new();

	public Task AddToRoom(AddToRoomParameters parameters)
	{
		var roomConnections = _connectionsByRoomId.GetOrAdd(parameters.RoomId, _ => new HashSet<string>());
		lock (roomConnections)
		{
			_participantIdByConnectionId[parameters.ConnectionId] = parameters.ParticipantId;
			roomConnections.Add(parameters.ConnectionId);
		}

		return Task.CompletedTask;
	}

	public Task RemoveFromRoom(string roomId, string connectionId)
	{
		if (_connectionsByRoomId.TryGetValue(roomId, out var roomConnections))
		{
			lock (roomConnections)
			{
				roomConnections.Remove(connectionId);
			}
		}

		return Task.CompletedTask;
	}

	public IReadOnlyCollection<string> GetConnections(string roomId)
	{
		_connectionsByRoomId.TryGetValue(roomId, out var roomConnections);
		if (roomConnections is null)
		{
			Console.WriteLine("CONNECTIONS: No Connections Exist");
			return [];
		}

			foreach (var test in roomConnections)
			{
				Console.WriteLine($"RoomConnectionManager.CONNECTION: {test}");
			}

		Console.WriteLine($"CONNECTIONS: {roomConnections}");

		lock (roomConnections)
		{
			return roomConnections.ToList();
		}
	}
}