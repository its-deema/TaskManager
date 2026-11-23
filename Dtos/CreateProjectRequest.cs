using System.ComponentModel.DataAnnotations;
namespace TaskManager.Dtos;

public record CreateProjectRequest ([Required, MaxLength(100)] string Name);