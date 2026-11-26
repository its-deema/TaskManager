using System.ComponentModel.DataAnnotations;
namespace TaskManager.Dtos;

public record UpdateProjectRequest ([Required, MaxLength(100)] string Name);