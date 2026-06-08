using Microsoft.AspNetCore.Mvc;
using SprintRetroAPI.Data.UnitOfWork.Interfaces;
using SprintRetroAPI.DTOs;
using SprintRetroAPI.Factories.RoomFactory.DTOs;
using SprintRetroAPI.Factories.RoomFactory.Interfaces;
using SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;
using SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;
using SprintRetroAPI.Finders.RoomFinder.Interfaces;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;
using SprintRetroAPI.Services.BroadcastService.Interfaces;

namespace SprintRetroAPI.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController(
	IUnitOfWork unitOfWork,
	IRoomFactory roomFactory,
	IRoomRepository roomRepository,
	IRoomViewModelFactory roomViewModelFactory,
	IRoomFinder roomFinder,
	IBroadcastService broadcastService
) : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IRoomFactory _roomFactory = roomFactory;
	private readonly IRoomRepository _roomRepository = roomRepository;
	private readonly IRoomViewModelFactory _roomViewModelFactory = roomViewModelFactory;
	private readonly IRoomFinder _roomFinder = roomFinder;
	private readonly IBroadcastService _broadcastService = broadcastService;

	[HttpPost]
	public async Task<ActionResult<RoomViewModel>> Create([FromBody] CreateRoomRequest request)
	{
		var room = _roomFactory.FromDTO(
			new RoomFactoryDTO
			{
				Name = request.Name
			}
		);

		foreach(var column in request.Columns)
		{
			room.AddColumn(column.Title, column.Position);
		}

		if (!string.IsNullOrEmpty(request.ParticipantName))
		{
			room.AddParticipant(request.ParticipantName);
		}

		_roomRepository.Upsert(room);

		await _unitOfWork.SaveChanges();

		await _broadcastService.RoomUpdated(room);

		return Ok(_roomViewModelFactory.FromRoom(room));
	}

	[HttpGet("{roomId:guid}")]
	public async Task<ActionResult<RoomViewModel>> Get(Guid roomId)
	{
		var room = await _roomFinder.ById(roomId);
		if (room is null)
		{
			return NotFound();
		}

		return Ok(_roomViewModelFactory.FromRoom(room));
	}
}