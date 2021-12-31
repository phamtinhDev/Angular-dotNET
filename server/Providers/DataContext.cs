using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Providers;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
}