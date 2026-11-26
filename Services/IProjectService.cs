using TaskManager.Models;
using TaskManager.Dtos;
namespace TaskManager.Services;

public interface IProjectService
{
    Task<List<ProjectResponse>> GetAllAsync();
    Task<ProjectResponse?> GetByIdAsync(int id);
    Task<ProjectResponse> CreateAsync(string name);
    Task<bool> UpdateAsync(int id, string name);
    Task<bool> DeleteAsync(int id);
}