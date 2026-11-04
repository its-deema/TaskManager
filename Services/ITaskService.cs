using TaskManager.Models;

namespace TaskManager.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetTasksAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(string title);
    Task<bool> UpdateAsync(int id, string title, bool isCompleted);
    Task<bool> DeleteAsync(int id);
}