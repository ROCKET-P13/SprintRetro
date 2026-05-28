using Microsoft.EntityFrameworkCore;
using SprintRetroAPI.Data;
using SprintRetroAPI.Data.UnitOfWork;
using SprintRetroAPI.Data.UnitOfWork.Interfaces;
using SprintRetroAPI.Factories.RoomFactory;
using SprintRetroAPI.Factories.RoomFactory.Interfaces;
using SprintRetroAPI.Factories.RoomViewModelFactory;
using SprintRetroAPI.Factories.RoomViewModelFactory.Interfaces;
using SprintRetroAPI.Finders.RoomFinder;
using SprintRetroAPI.Finders.RoomFinder.Interfaces;
using SprintRetroAPI.Repositories.RoomRepository;
using SprintRetroAPI.Repositories.RoomRepository.Interfaces;

namespace SprintRetroAPI;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<AppDatabaseContext>(options =>
		{
			options.UseNpgsql(
				connectionString,
				npgsqlOptions =>
				{
					npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
				}
			);
		});

		services.AddScoped<IRoomRepository, RoomRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.AddScoped<IRoomFactory, RoomFactory>();
		services.AddScoped<IRoomViewModelFactory, RoomViewModelFactory>();

		services.AddScoped<IRoomFinder, RoomFinder>();


		services.AddControllers();

		return services;
	}
}