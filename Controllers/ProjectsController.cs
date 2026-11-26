using Microsoft.AspNetCore.Mvc;
using TaskManager.Services;
using TaskManager.Dtos;

namespace TaskManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController (IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectResponse>>> GetProjects()
    {
        var projects = await _projectService.GetAllAsync();    
        return Ok(projects);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectResponse>> GetProjectById(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project is null) return NotFound();

        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponse>> CreateProject(CreateProjectRequest request)
    {
        var project = await _projectService.CreateAsync(request.Name);

        return CreatedAtAction(
            nameof(GetProjectById),
            new {id = project.Id},
            project
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProject(int id, UpdateProjectRequest request)
    {
        if(!await _projectService.UpdateAsync(id, request.Name))
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        if(!await _projectService.DeleteAsync(id))
            return NotFound();
        return NoContent();
    }
}