using SprintRetroAPI.Entities;
using SprintRetroAPI.Factories.RoomFactory.DTOs;

namespace SprintRetroAPI.Factories.RoomFactory.Interfaces;

public interface IRoomFactory
{
	Room FromDTO(RoomFactoryDTO dto);
}