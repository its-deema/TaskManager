using TaskManager.Services;
using TaskManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register services in the DI container (like Spring's @Service / @Bean)
// ── Phase 1: "What does my app HAVE?"
builder.Services.AddControllers();               // turns on controllers
builder.Services.AddOpenApi();                    // API docs (like springdoc)
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// ── Phase 2: "How are requests HANDLED?"
if (app.Environment.IsDevelopment())
    app.MapOpenApi();                             // /openapi/v1.json

app.MapControllers();                             // routes requests to controllers
app.Run();
