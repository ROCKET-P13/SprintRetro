using Microsoft.AspNetCore.Mvc;
using SprintRetroAPI.Data.UnitOfWork.Interfaces;
using SprintRetroAPI.DTOs;
using SprintRetroAPI.Entities;
using SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;
using SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;

namespace SprintRetroAPI.Controllers;

[ApiController]
[Route("columns")]
public class CoulumnsController(
	IUnitOfWork unitOfWork,
	IRoomRepository roomRepository,
	IRoomViewModelFactory roomViewModelFactory
) : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IRoomRepository _roomRepository = roomRepository;
	private readonly IRoomViewModelFactory _roomViewModelFactory = roomViewModelFactory;

	[HttpPost]
	public async Task<ActionResult<RoomViewModel>> Create([FromBody] CreateColumnRequest request)
	{
		var room = await _roomRepository.FindById(request.RoomId);
		if (room is null)
		{
			return NotFound("Room not found");
		}

		room.AddColumn(
			new Column
			{
				Id = Guid.NewGuid(),
				RoomId = room.Id,
				Title = request.Title,
				Position = request.Position,
			}
		);
		
		var columns = _unitOfWork.DbContext.ChangeTracker.Entries<Column>()
			.Select(x => new
			{
				x.Entity.Id,
				x.State,
				x.DebugView
			});

		foreach (var c in columns)
		{
			Console.WriteLine($"Column {c.Id} => {c.State}");
		}
		await _unitOfWork.SaveChanges();

		return Ok(_roomViewModelFactory.FromRoom(room));
	}
}