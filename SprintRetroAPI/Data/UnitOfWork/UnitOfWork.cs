using SprintRetroAPI.Data.UnitOfWork.Interfaces;

namespace SprintRetroAPI.Data.UnitOfWork;

public sealed class UnitOfWork(AppDatabaseContext databaseContext) : IUnitOfWork
{
	private readonly AppDatabaseContext _databaseContext = databaseContext;

	public async Task SaveChanges(CancellationToken cancellationToken)
	{
		await _databaseContext.SaveChangesAsync(cancellationToken);
	}
}