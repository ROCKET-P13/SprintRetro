using System.Text.Json;
using SprintRetroAPI.Contracts.ClientMessages;
using SprintRetroAPI.Services.RoomConnectionManager.Interfaces;
using SprintRetroAPI.Services.WebSockets.LocalWebSocketConnectionManager.Interfaces;

namespace SprintRetroAPI.Services.WebSockets.Handlers;

public class JoinRoomHandler(IRoomConnectionManager roomConnectionManager)
{
	private readonly IRoomConnectionManager _roomConnectionManager = roomConnectionManager;

	public async Task Handle(string connectionId, JsonElement payload)
	{
		var message = payload.Deserialize<JoinRoomMessage>();

		if (message is null)
		{
			return;
		}

		await _roomConnectionManager.AddToRoom(message.RoomId.ToString(), connectionId);
	}
}