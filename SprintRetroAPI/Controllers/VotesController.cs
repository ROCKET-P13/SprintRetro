using Microsoft.AspNetCore.Mvc;
using SprintRetroAPI.Data.UnitOfWork.Interfaces;
using SprintRetroAPI.DTOs.Request;
using SprintRetroAPI.DTOs.Response;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;
using SprintRetroAPI.Services.BroadcastService.Interfaces;

namespace SprintRetroAPI.Controllers;

[ApiController]
[Route("api/rooms/{roomId:guid}/votes")]
public class VotesController(
	IUnitOfWork unitOfWork,
	IRoomRepository roomRepository,
	IBroadcastService broadcastService
) : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IRoomRepository _roomRepository = roomRepository;
	private readonly IBroadcastService _broadcastService = broadcastService;

	[HttpPost]
	public async Task<ActionResult<VoteCommentResponse>> Add([FromRoute] Guid roomId, [FromBody] VoteCommentRequest request)
	{
		var room = await _roomRepository.FindById(roomId);
		if (room is null)
		{
			return NotFound("Room not found");
		}

		var vote = room.AddVote(request);

		await _unitOfWork.SaveChanges();

		await _broadcastService.RoomUpdated(room);

		return Ok(
			new VoteCommentResponse
			{
				Id = vote.Id,
				ColumnId = vote.Comment.ColumnId,
				CommentId = vote.CommentId,
				ParticipantName = vote.Participant.Name
			}
		);
	}

	[HttpDelete("{voteId:guid}")]
	public async Task<ActionResult> Remove([FromRoute] Guid roomId, Guid voteId)
	{
		var room = await _roomRepository.FindById(roomId);
		if (room is null)
		{
			return NotFound("Room not found");
		}

		room.RemoveVote(voteId);

		await _unitOfWork.SaveChanges();

		await _broadcastService.RoomUpdated(room);

		return Ok();
	}
}