using TaskManager.Models;
using TaskManager.Data;
using Microsoft.EntityFrameworkCore;
using TaskManager.Exceptions;


namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;
    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    // helper method to throw an exception if project doesn't exist
    private async Task EnsureProjectExistsAsync(int? projectId){
        if (projectId is not null && !await _db.Projects.AnyAsync(p => p.Id == projectId))
            throw new BadRequestException($"Project {projectId} does not exist."); 
    } 

    // method that returns tasks ,
    // the => is a short way to write a method with a single return statement
    public async Task<List<TaskItem>> GetTasksAsync() =>
        await _db.Tasks.ToListAsync(); //_db.Tasks works like a list, but it’s a query. Add .ToList() to run it.

    public async Task<TaskItem?> GetByIdAsync(int id) =>
    await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<TaskItem> CreateAsync(string title, int? projectId )
    {
        await EnsureProjectExistsAsync(projectId); // check if the projectId exist or throw an exception
        var task = new TaskItem { Title = title , ProjectId = projectId};
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync(); // like commit
        return task;
    }

    public async Task<bool> UpdateAsync(int id, string title, bool isCompleted, int? projectId )
    {
        var task = await GetByIdAsync(id);
        if (task is null) return false;

        await EnsureProjectExistsAsync(projectId); // check if the projectId exist
        
        task.Title = title;
        task.IsCompleted = isCompleted;
        task.ProjectId = projectId;
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
