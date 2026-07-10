using Microsoft.AspNetCore.Mvc;
using SprintRetroAPI.Data.UnitOfWork.Interfaces;
using SprintRetroAPI.DTOs.Request;
using SprintRetroAPI.DTOs.Response;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;
using SprintRetroAPI.Services.BroadcastService.Interfaces;

namespace SprintRetroAPI.Controllers;

[ApiController]
[Route("api/rooms/{roomId:guid}/columns")]
public class CoulumnsController(
	IUnitOfWork unitOfWork,
	IRoomRepository roomRepository,
	IBroadcastService broadcastService
) : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IRoomRepository _roomRepository = roomRepository;
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
	public async Task<ActionResult<UpdateColumnPositionsResponse>> UpdatePosition([FromRoute] Guid roomId, [FromBody] UpdateColumnPositionsRequest request)
	{
		var room = await _roomRepository.FindById(roomId);

		if (room is null)
		{
			return NotFound("Room not found");
		}

		room.UpdateColumns(request.Columns);

		await _unitOfWork.SaveChanges();

		await _broadcastService.RoomUpdated(room);

		var updatedColumnIds = request.Columns
			.Select(c => c.Id)
			.ToHashSet();

		return Ok(
			new UpdateColumnPositionsResponse
			{
				Columns = [
				..room.Columns
					.Where(column => updatedColumnIds.Contains(column.Id))
					.OrderBy(column => column.Position)
					.Select(column =>
						new UpdateColumnPositionsResponseColumn
						{
							Id = column.Id,
							Title = column.Title,
							Position = column.Position
						})
				]
			}
		);
	}

	[HttpPatch("{columnId:guid}")]
	public async Task<ActionResult> UpdateTitle([FromRoute] Guid roomId, Guid columnId, [FromBody] UpdateColumnTitleRequest request)
	{
		var room = await _roomRepository.FindById(roomId);

		if (room is null)
		{
			return NotFound("Room not found");
		}

		room.UpdateColumnTitle(columnId, request.Title);

		await _unitOfWork.SaveChanges();
		await _broadcastService.RoomUpdated(room);

		return Ok(
			new UpdateColumnTitleResponse
			{
				Id = columnId,
				Title = request.Title
			}
		);
	}

	[HttpDelete("{columnId:guid}")]
	public async Task<ActionResult> Delete([FromRoute] Guid roomId, Guid columnId)
	{
		var room = await _roomRepository.FindById(roomId);

		if (room is null)
		{
			return NotFound("Room not found");
		}

		room.RemoveColumn(columnId);

		await _unitOfWork.SaveChanges();

		await _broadcastService.RoomUpdated(room);

		return Ok();
	}
}