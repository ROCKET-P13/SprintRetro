namespace SprintRetroAPI.Data.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
	AppDatabaseContext DbContext { get; }

	Task SaveChanges();
}