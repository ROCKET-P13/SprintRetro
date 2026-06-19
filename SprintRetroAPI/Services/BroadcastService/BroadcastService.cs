using System.Text;
using System.Text.Json;
using SprintRetroAPI.Contracts.ServerMessages;
using SprintRetroAPI.Entities;
using SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;
using SprintRetroAPI.Serialization;
using SprintRetroAPI.Services.BroadcastService.Interfaces;
using SprintRetroAPI.Services.RoomConnectionManager.Interfaces;
using SprintRetroAPI.Services.WebSockets.WebSocketPublisher.Interfaces;

namespace SprintRetroAPI.Services.BroadcastService;

public class BroadcastService(
	IRoomViewModelFactory roomViewModelFactory,
	IWebSocketPublisher webSocketPublisher,
	IRoomConnectionManager roomConnectionManager
) : IBroadcastService
{
	private readonly IRoomViewModelFactory _roomViewModelFactory = roomViewModelFactory;
	private readonly IWebSocketPublisher _webSocketPublisher = webSocketPublisher;
	private readonly IRoomConnectionManager _roomConnectionManager = roomConnectionManager;

	public async Task RoomUpdated(Room room)
	{
		var payload = new RoomUpdatedMessage{ Payload = _roomViewModelFactory.FromRoom(room) };

		var connectionIds = _roomConnectionManager.GetConnections(room.Id.ToString());

		var tasks = connectionIds.Select(id => _webSocketPublisher.PublishToConnection(id, payload));

		await Task.WhenAll(tasks);
	}

    public async Task SendToConnectionAsync(
        string connectionId,
        object payload
		)
		{
			// var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload, AppJsonSerializerOptions.ApplicationDefault)).AsMemory();
			await _webSocketPublisher.PublishToConnection(connectionId, payload);
		}
	}