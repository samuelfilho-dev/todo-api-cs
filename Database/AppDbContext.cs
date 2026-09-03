using Microsoft.EntityFrameworkCore;
using TodoCs.Models;

namespace TodoCs.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Todo> Todos => Set<Todo>();
}