# Task Manager API

A simple REST API for managing tasks. I'm building it to learn .NET the practical way, the way it's used in real projects (coming from Spring Boot).

## Tech

- ASP.NET Core (.NET 10)
- EF Core + SQLite
- OpenAPI

## What it does

- CRUD for tasks
- Request validation
- Async from the controller down to the database
- Projects: one project has many tasks (endpoints in progress)

## Run it

```bash
dotnet ef database update
dotnet run
```

Then open http://localhost:5186/api/tasks, or use `TaskManager.http` to try the endpoints.

## Next

- Project endpoints
- Auth (JWT)
- Tests
- Docker + PostgreSQL
