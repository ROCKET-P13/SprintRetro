using System.Text.Json;
using SprintRetroAPI.Contracts.ClientMessages;
using SprintRetroAPI.Serialization;
using SprintRetroAPI.Services.RoomConnectionManager.Interfaces;
using SprintRetroAPI.Services.RoomConnectionManager.Parameters;
using SprintRetroAPI.Services.WebSockets.Handlers.Interfaces;

namespace SprintRetroAPI.Services.WebSockets.Handlers;

public class JoinRoomHandler(IRoomConnectionManager roomConnectionManager) : IHandler
{
	private readonly IRoomConnectionManager _roomConnectionManager = roomConnectionManager;

	public async Task<object?> Handle(string connectionId, JsonElement payload)
	{
		var message = JsonSerializer.Deserialize<JoinRoomMessage>(payload, AppJsonSerializerOptions.ApplicationDefault);

		if (message is null)
		{
			throw new Exception("Message is empty");
		}

		await _roomConnectionManager.AddToRoom(
			new AddToRoomParameters
			{
				RoomId = message.RoomId.ToString(),
				ConnectionId = connectionId,
				ParticipantId = message.ParticipantId.ToString(),
			}
		);

		return null;
	}
}