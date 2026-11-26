using TaskManager.Models;
using TaskManager.Data;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dtos;

namespace TaskManager.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _db; //to acess the db

    public ProjectService (AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ProjectResponse>> GetAllAsync()
    {
       var projects = await _db.Projects
            .Include(p => p.Tasks)      // load each project WITH its tasks
            .ToListAsync();             // run the query → List<Project>
        
        return projects.Select(ProjectResponse.FromEntity).ToList(); // convert to DTOs
    }


    public async Task<ProjectResponse?> GetByIdAsync(int id)
    {
        var project = await _db.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return project is null? null : ProjectResponse.FromEntity(project);
    }

    public async Task<ProjectResponse> CreateAsync(string name)
    {
        var project = new Project { Name = name };
        _db.Projects.Add(project);
        await _db.SaveChangesAsync();
        return ProjectResponse.FromEntity(project); // convert it from project model into projectResponse dto
    }

    public async Task<bool> UpdateAsync(int id, string name)
    {   
        //cause GetByIdAsync return a ProjectResponse and we want project entity 
        var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == id);   // a tracked Project entity
        if (project is null) return false;

        project.Name = name;
        await _db.SaveChangesAsync();
        return true;

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project =  await _db.Projects.FirstOrDefaultAsync(p=> p.Id == id);
        if (project is null) return false;

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();
        return true;
    }
}