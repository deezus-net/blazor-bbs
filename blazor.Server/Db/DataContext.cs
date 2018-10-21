using blazor.Shared;
using Microsoft.EntityFrameworkCore;

namespace blazor.Server.Db
{
    public class DataContext : DbContext
    {
        public DbSet<BoardThread> BoardThreads { get; set; }
        public DbSet<BoardResponse> BoardResponses { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}