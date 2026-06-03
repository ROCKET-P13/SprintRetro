using System.Text.Json;
using SprintRetroAPI.Contracts.ClientMessages;
using SprintRetroAPI.Serialization;
using SprintRetroAPI.Services.RoomConnectionManager.Interfaces;
using SprintRetroAPI.Services.RoomConnectionManager.Parameters;

namespace SprintRetroAPI.Services.WebSockets.Handlers;

public class JoinRoomHandler(IRoomConnectionManager roomConnectionManager)
{
	private readonly IRoomConnectionManager _roomConnectionManager = roomConnectionManager;

	public async Task Handle(string connectionId, JsonElement payload)
	{
		var message = JsonSerializer.Deserialize<JoinRoomMessage>(payload, AppJsonSerializerOptions.ApplicationDefault);

		if (message is null)
		{
			return;
		}

		await _roomConnectionManager.AddToRoom(
			new AddToRoomParameters
			{
				RoomId = message.RoomId.ToString(),
				ConnectionId = connectionId,
				ParticipantId = message.ParticipantId.ToString(),
			}
		);
	}
}