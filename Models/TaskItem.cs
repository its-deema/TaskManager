namespace TaskManager.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; }

    public int? ProjectId { get; set; }      // the foreign key column
    public Project? Project { get; set; }    // navigation property: lets you write task.Project.Name
}
