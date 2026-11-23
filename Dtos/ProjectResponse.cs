using TaskManager.Models;
namespace TaskManager.Dtos; 

public record ProjectResponse(int Id, string Name, List<TaskResponse> Tasks)
{
    public static ProjectResponse FromEntity(Project project)=>
        new(project.Id, project.Name, project.Tasks.Select(TaskResponse.FromEntity).ToList());
}