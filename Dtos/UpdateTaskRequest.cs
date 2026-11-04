using System.ComponentModel.DataAnnotations;
namespace TaskManager.Dtos;

public record UpdateTaskRequest([Required, MaxLength(200)]string Title, bool IsCompleted);