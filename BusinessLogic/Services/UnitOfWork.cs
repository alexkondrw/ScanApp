using BusinessLogic.Abstractions;
using DataAccess;
using DataAccess.Abstractions;
using DataAccess.Repositories;

namespace BusinessLogic.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Hash = new HashRepository(context);
    }

    public IHashRepository Hash { get; }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool _disposed;
    private void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();

        _disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}