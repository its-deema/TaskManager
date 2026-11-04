using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data;

// DbContext represents a session with the database: it tracks changes and saves them.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>(); //create a table called Tasks,
    //Query it like a list _db.Tasks.FirstOrDefault(...)

    public DbSet<Project> Projects => Set<Project>(); //create a table called Projects,
}