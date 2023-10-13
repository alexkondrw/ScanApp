using DataAccess.Abstractions;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class HashRepository : IHashRepository
{
    private readonly AppDbContext _context;

    public HashRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Hash hash)
    {
        await _context.Hashes.AddAsync(hash);
    }

    public async Task UpdateAsync(Hash hash)
    {
        await Task.Run(() => _context.Hashes.Update(hash));
    }

    public async Task<Hash?> FindAsync(string filePath)
    {
        return await _context.Hashes.FindAsync(filePath);
    }
}