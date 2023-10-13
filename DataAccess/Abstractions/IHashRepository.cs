using DataAccess.Models;

namespace DataAccess.Abstractions;

public interface IHashRepository
{
    Task AddAsync(Hash hash);
    Task UpdateAsync(Hash hash);
    Task<Hash?> FindAsync(string filePath);
}