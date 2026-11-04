using System.ComponentModel.DataAnnotations;
namespace TaskManager.Dtos;

public record CreateTaskRequest([Required, MaxLength(200)] string Title);