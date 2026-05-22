using Microsoft.AspNetCore.Mvc;
using SprintRetroAPI.Data.UnitOfWork.Interfaces;
using SprintRetroAPI.DTOs;
using SprintRetroAPI.Entities;
using SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;
using SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;

namespace SprintRetroAPI.Controllers;

[ApiController]
[Route("comments")]
public class CommentsController(
	IUnitOfWork unitOfWork,
	IRoomRepository roomRepository,
	IRoomViewModelFactory roomViewModelFactory
): ControllerBase
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IRoomRepository _roomRepository = roomRepository;
	private readonly IRoomViewModelFactory _roomViewModelFactory = roomViewModelFactory;

	[HttpPost]
	public async Task<ActionResult<RoomViewModel>> Create([FromBody] CreateCommentRequest request)
	{
		var room = await _roomRepository.FindById(request.RoomId);
		if (room is null)
		{
			return NotFound("Room not found");
		}

		room.AddComment(request);

		await _unitOfWork.SaveChanges();

		return Ok(_roomViewModelFactory.FromRoom(room));
		
	}
}