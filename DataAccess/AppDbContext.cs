using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class AppDbContext : DbContext
{
    public DbSet<Hash> Hashes { get; set; } = null!;
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
}