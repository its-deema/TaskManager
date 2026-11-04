using TaskManager.Models;
namespace TaskManager.Dtos; 
public record TaskResponse(int Id, string Title, bool IsCompleted)
{
    // Converts a TaskItem object into a TaskResponse object
    public static TaskResponse FromEntity(TaskItem task) =>
        new(task.Id, task.Title, task.IsCompleted);
}