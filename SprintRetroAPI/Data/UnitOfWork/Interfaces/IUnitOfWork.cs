namespace SprintRetroAPI.Data.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken);
}