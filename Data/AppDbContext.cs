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

    // configures relationships that conventions don't cover
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Project)          // a task has one project
            .WithMany(p => p.Tasks)          // a project has many tasks
            .HasForeignKey(t => t.ProjectId) // linked by ProjectId
            .OnDelete(DeleteBehavior.Cascade);
    }
}