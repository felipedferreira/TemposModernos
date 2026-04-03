# Acme.Cinedex.MoviesService

An ASP.NET Core 10.0 microservice for managing movies, part of the Acme Cinedex platform.

## Tech Stack

- .NET 10.0 / ASP.NET Core
- OpenAPI
- Entity Framework Core + PostgreSQL
- Docker

## Architecture

The project follows a **Clean Architecture** with four layers:

```
src/
├── Presentation/
│   └── WebService              → ASP.NET Core Web API (HTTP entry point)
├── Shared/
│   ├── Application             → Use cases and business logic orchestration
│   ├── Application.Abstractions → Interfaces and contracts for the application layer
│   └── Domain                  → Core entities and business rules (no dependencies)
└── Adapters/
    └── Persistence             → Data access via Entity Framework Core + PostgreSQL
```

**Dependency flow:** Presentation → Application → Application.Abstractions → Domain

The Domain layer has zero external dependencies, keeping business rules isolated from infrastructure concerns. The Adapters layer provides concrete implementations for abstractions defined in the inner layers.

## Branching Conventions

**Format:** `<type>/<short-description>`

| Type | Use for |
|------|---------|
| `feature/` | New functionality |
| `fix/` | Bug fixes |
| `chore/` | Config, CI, tooling, docs |
| `refactor/` | Code restructuring |

**Rules:**
- Lowercase with hyphens: `feature/add-movie-endpoint`
- If using a ticket tracker, prefix with the ticket ID: `feature/CIN-42-add-movie-endpoint`
- Branch from `main`, merge back to `main` via pull request