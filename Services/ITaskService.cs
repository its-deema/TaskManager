using TaskManager.Models;

namespace TaskManager.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetTasksAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(string title, int? projectId);
    Task<bool> UpdateAsync(int id, string title, bool isCompleted, int? projectId);
    Task<bool> DeleteAsync(int id);
}