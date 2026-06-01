using System.Text;
using System.Text.Json;
using Amazon.ApiGatewayManagementApi;
using Amazon.ApiGatewayManagementApi.Model;
using SprintRetroAPI.Contracts.ServerMessages;
using SprintRetroAPI.Entities;
using SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;
using SprintRetroAPI.Services.BroadcastService.Interfaces;

namespace SprintRetroAPI.Services.BroadcastService;

public class BroadcastService(
	IRoomViewModelFactory roomViewModelFactory,
	IAmazonApiGatewayManagementApi apiGateway
) : IBroadcastService
{
	private readonly IRoomViewModelFactory _roomViewModelFactory = roomViewModelFactory;
	private readonly IAmazonApiGatewayManagementApi _apiGateway = apiGateway;

	public async Task RoomUpdated(Room room)
	{
		var payload = JsonSerializer.Serialize(
			new RoomUpdatedMessage
			{
				Room = _roomViewModelFactory.FromRoom(room)
			}
		);

		var bytes = Encoding.UTF8.GetBytes(payload);

		foreach (var participant in room.Participants)
		{
			if (string.IsNullOrWhiteSpace(participant.ConnectionId))
			{
				continue;
			}

			await _apiGateway.PostToConnectionAsync(
				new PostToConnectionRequest
				{
					ConnectionId = participant.ConnectionId,
					Data = new MemoryStream(bytes)
				}
			);
		}
	}
}