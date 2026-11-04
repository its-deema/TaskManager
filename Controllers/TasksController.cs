using Microsoft.AspNetCore.Mvc;
using TaskManager.Services;
using TaskManager.Dtos;

namespace TaskManager.Controllers;

[ApiController]
[Route("api/[controller]")] // defines the URL route [controller] is replaced with the controller's name
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<List<TaskResponse>> GetTasks()
    {
        var tasks = await _taskService.GetTasksAsync();
        return tasks.Select(TaskResponse.FromEntity).ToList();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskResponse>> GetTaskById(int id) // Returns a TaskItem -> (TaskResponsey) or an HTTP response
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task is null) return NotFound();

        return Ok(TaskResponse.FromEntity(task));
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTask(CreateTaskRequest request)
    {
        var task = await _taskService.CreateAsync(request.Title);

        return CreatedAtAction(
        nameof(GetTaskById),     // Action to link to
        new { id = task.Id },    // ID of the newly created task
        TaskResponse.FromEntity(task)          // Return the created task in the response body
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, UpdateTaskRequest request)
    {
        if (!await _taskService.UpdateAsync(id, request.Title, request.IsCompleted))
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        if (!await _taskService.DeleteAsync(id))
            return NotFound();

        return NoContent();
    }
}