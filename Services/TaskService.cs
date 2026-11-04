using TaskManager.Models;
using TaskManager.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;
    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    // method that returns tasks ,
    // the => is a short way to write a method with a single return statement
    public async Task<List<TaskItem>> GetTasksAsync() =>
        await _db.Tasks.ToListAsync(); //_db.Tasks works like a list, but it’s a query. Add .ToList() to run it.

    public async Task<TaskItem?> GetByIdAsync(int id) =>
    await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<TaskItem> CreateAsync(string title)
    {
        var task = new TaskItem { Title = title };
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync(); // like commit
        return task;
    }

    public async Task<bool> UpdateAsync(int id, string title, bool isCompleted)
    {
        var task = await GetByIdAsync(id);
        if (task is null) return false;
        
        task.Title = title;
        task.IsCompleted = isCompleted;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);
        if (task is null) return false;

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return true;
    }

}
