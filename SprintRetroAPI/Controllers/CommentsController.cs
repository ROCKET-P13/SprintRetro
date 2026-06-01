using Microsoft.AspNetCore.Mvc;
using SprintRetroAPI.Data.UnitOfWork.Interfaces;
using SprintRetroAPI.DTOs;
using SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;
using SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;
using SprintRetroAPI.Services.BroadcastService.Interfaces;

namespace SprintRetroAPI.Controllers;

[ApiController]
[Route("api/rooms/{roomId:guid}/comments")]
public class CommentsController(
	IUnitOfWork unitOfWork,
	IRoomRepository roomRepository,
	IRoomViewModelFactory roomViewModelFactory,
	IBroadcastService broadcastService
): ControllerBase
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IRoomRepository _roomRepository = roomRepository;
	private readonly IRoomViewModelFactory _roomViewModelFactory = roomViewModelFactory;
	private readonly IBroadcastService _broadcastService = broadcastService;

	[HttpPost]
	public async Task<ActionResult<RoomViewModel>> Create([FromRoute] Guid roomId, [FromBody] CreateCommentRequest request)
	{
		var room = await _roomRepository.FindById(roomId);
		if (room is null)
		{
			return NotFound("Room not found");
		}

		room.AddComment(request);

		await _unitOfWork.SaveChanges();

		await _broadcastService.RoomUpdated(room);

		return Ok(_roomViewModelFactory.FromRoom(room));
	}
}