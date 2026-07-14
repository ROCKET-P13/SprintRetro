using Microsoft.AspNetCore.Mvc;
using SprintRetroAPI.Data.UnitOfWork.Interfaces;
using SprintRetroAPI.DTOs.Request;
using SprintRetroAPI.DTOs.Response;
using SprintRetroAPI.Entities;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;
using SprintRetroAPI.Services.BroadcastService.Interfaces;

namespace SprintRetroAPI.Controllers;

[ApiController]
[Route("api/rooms/{roomId:guid}/comments")]
public class CommentsController(
	IUnitOfWork unitOfWork,
	IRoomRepository roomRepository,
	IBroadcastService broadcastService
): ControllerBase
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IRoomRepository _roomRepository = roomRepository;
	private readonly IBroadcastService _broadcastService = broadcastService;

	[HttpPost]
	public async Task<ActionResult<Comment>> Create([FromRoute] Guid roomId, [FromBody] CreateCommentRequest request)
	{
		var room = await _roomRepository.FindById(roomId);
		if (room is null)
		{
			return NotFound("Room not found");
		}

		var comment = room.AddComment(request);

		await _unitOfWork.SaveChanges();
		await _broadcastService.RoomUpdated(room);



		return Ok(
			new CreateCommentResponse
			{
				Id = comment.Id,
				Body = comment.Body,
				VoteCount = 0,
				CreatedBy = room.Participants.First(participant => participant.Id == comment.ParticipantId).Name,
			}
		);
	}

	[HttpPost("{commentId:guid}/merge")]
	public async Task<ActionResult<Comment>> Merge([FromRoute] Guid roomId, Guid commentId, [FromBody] MergeCommentsRequest request)
	{
		
		var room = await _roomRepository.FindById(roomId);
		if (room is null)
		{
			return NotFound("Room not found");
		}

		var mergedComment = room.MergeComments(commentId, request.CommentId);

		await _unitOfWork.SaveChanges();
		await _broadcastService.RoomUpdated(room);

		return Ok(mergedComment);
	}
}