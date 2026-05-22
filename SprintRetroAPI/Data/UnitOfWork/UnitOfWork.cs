using SprintRetroAPI.Data.UnitOfWork.Interfaces;

namespace SprintRetroAPI.Data.UnitOfWork;

public sealed class UnitOfWork(AppDatabaseContext databaseContext) : IUnitOfWork
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;
	public async Task SaveChanges()
	{
		await _databaseContext.SaveChangesAsync();
	}
	public AppDatabaseContext DbContext { get; } = databaseContext;
}