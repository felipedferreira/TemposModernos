# Presentation Layer Architecture

This project follows a **vertical slice architecture** in the Presentation layer. Each API resource is organized by operation rather than by technical concern.

## Directory Structure

```
WebService/
  API/
    {Resource}/
      {Operation}/
        {Operation}{Resource}Request.cs
        {Operation}{Resource}Response.cs
      {Resource}Controller.cs
```

### Example

```
API/
  Movies/
    Create/
      CreateMovieRequest.cs
      CreateMovieResponse.cs
    GetAll/
      GetAllMoviesResponse.cs
    GetById/
      GetByIdMovieResponse.cs
    Update/
      UpdateMovieRequest.cs
      UpdateMovieResponse.cs
    Delete/
      (no DTOs needed if returning 204)
    MoviesController.cs
```

## Guidelines

### Controller

- One controller per resource, placed at the resource directory root (e.g., `API/Movies/MoviesController.cs`).
- The controller is responsible for routing and delegating to application layer services.
- Use `[ApiController]` and `[Route("api/[controller]")]` attributes.

### Operation Directories

- Each operation (Create, GetAll, GetById, Update, Delete) gets its own directory under the resource.
- All DTOs (requests, responses) and mappers specific to that operation live inside its directory.
- Validators belong in the Application layer, not here.
- If an operation has no request or response DTOs (e.g., Delete returning 204), the directory can be omitted.

### Naming Conventions

- **Requests**: `{Operation}{Resource}Request` (e.g., `CreateMovieRequest`)
- **Responses**: `{Operation}{Resource}Response` (e.g., `GetAllMoviesResponse`)


### Namespaces

Namespaces follow the folder structure:

- `Acme.Cinedex.MoviesService.WebService.API.{Resource}` for the controller
- `Acme.Cinedex.MoviesService.WebService.API.{Resource}.{Operation}` for operation-specific types

### Why This Structure

- **Cohesion**: Everything related to a single operation is colocated. When working on "Create Movie", all relevant files are in one place.
- **Scalability**: Adding a new operation means adding a new directory — no risk of bloating shared folders.
- **Discoverability**: The folder structure communicates the API surface at a glance.
- **Isolation**: Changes to one operation don't touch files shared across operations, reducing merge conflicts.
