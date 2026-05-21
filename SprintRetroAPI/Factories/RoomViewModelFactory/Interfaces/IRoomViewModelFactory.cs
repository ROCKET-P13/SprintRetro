using SprintRetroAPI.Entities;
using SprintRetroAPI.Factories.RoomViewModelFactory.DTOs;

namespace SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;

public interface IRoomViewModelFactory
{
	RoomViewModel FromRoom(Room room);
}