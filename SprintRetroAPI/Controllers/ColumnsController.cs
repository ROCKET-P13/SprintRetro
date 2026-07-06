using Microsoft.AspNetCore.Mvc;
using SprintRetroAPI.Data.UnitOfWork.Interfaces;
using SprintRetroAPI.DTOs.Request;
using SprintRetroAPI.DTOs.Response;
using SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;
using SprintRetroAPI.Services.BroadcastService.Interfaces;

namespace SprintRetroAPI.Controllers;

[ApiController]
[Route("api/rooms/{roomId:guid}/columns")]
public class CoulumnsController(
	IUnitOfWork unitOfWork,
	IRoomRepository roomRepository,
	IRoomViewModelFactory roomViewModelFactory,
	IBroadcastService broadcastService
) : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IRoomRepository _roomRepository = roomRepository;
	private readonly IRoomViewModelFactory _roomViewModelFactory = roomViewModelFactory;
	private readonly IBroadcastService _broadcastService = broadcastService;

	[HttpPost]
	public async Task<ActionResult<CreateColumnResponse>> Create([FromRoute] Guid roomId, [FromBody] CreateColumnRequest request)
	{
		var room = await _roomRepository.FindById(roomId);
		if (room is null)
		{
			return NotFound("Room not found");
		}

		var column = room.AddColumn(request.Title, request.Position);

		await _unitOfWork.SaveChanges();

		await _broadcastService.RoomUpdated(room);

		return Ok(
			new CreateColumnResponse
			{
				Id = column.Id,
				Title = column.Title,
				Position = column.Position,
			}
		);
	}

	[HttpPatch]
	public async Task<ActionResult<UpdateColumnsResponse>> Update([FromRoute] Guid roomId, [FromBody] UpdateColumnsRequest request)
	{
		var room = await _roomRepository.FindById(roomId);

		if (room is null)
		{
			return NotFound("Room not found");
		}

		room.UpdateColumns(request.Columns);

		await _unitOfWork.SaveChanges();

		await _broadcastService.RoomUpdated(room);


		return new UpdateColumnsResponse
		{
			Columns = [
			.. room.Columns.OrderBy(c => c.Position).Select(column =>
				new UpdateColumnsResponseColumn
				{
					Id = column.Id,
					Title = column.Title,
					Position = column.Position
				})
			]
		};
	}
}