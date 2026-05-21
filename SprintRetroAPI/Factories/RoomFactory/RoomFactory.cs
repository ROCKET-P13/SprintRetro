using SprintRetroAPI.Entities;
using SprintRetroAPI.Factories.RoomFactory.DTOs;
using SprintRetroAPI.Factories.RoomFactory.Interfaces;

namespace SprintRetroAPI.Factories.RoomFactory;

public class RoomFactory : IRoomFactory
{
	public Room FromDTO(RoomFactoryDTO dto)
	{
		return new Room
		{
			Id = Guid.NewGuid(),
			Name = dto.Name,
			CreatedAt = DateTimeOffset.UtcNow,
		};
	}
}