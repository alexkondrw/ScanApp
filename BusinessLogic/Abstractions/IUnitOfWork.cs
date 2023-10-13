using DataAccess.Abstractions;

namespace BusinessLogic.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IHashRepository Hash { get; }
    Task SaveChangesAsync();
}