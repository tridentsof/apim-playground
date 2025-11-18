# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

This is a .NET 9 RESTful API project designed for learning and experimentation with Azure API Management (APIM) integration. It's a simple Items CRUD API with no authentication, using in-memory data storage and comprehensive Swagger/OpenAPI documentation.

## Common Commands

### Build and Run
```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application (starts at http://localhost:5000 and https://localhost:5001)
dotnet run

# Run with hot reload for development
dotnet watch run
```

### Testing and Verification
There are no automated tests in this project yet. To verify functionality:
- Access Swagger UI at `https://localhost:5001/swagger`
- Use the health check endpoint: `curl https://localhost:5001/health`
- Test CRUD operations via curl or Swagger UI

### Cleaning
```bash
# Clean build artifacts
dotnet clean
```

## Architecture Overview

### Layered Structure
The project follows a simple layered architecture:

1. **Controllers Layer** (`Controllers/`)
   - `ItemsController`: Handles CRUD operations for items at `/api/items`
   - `HealthController`: Health check endpoint at `/health`
   - Controllers use Swagger annotations for API documentation

2. **Service Layer** (`Services/`)
   - `ItemService`: Business logic and in-memory data management
   - Singleton service that maintains state in a `List<Item>`
   - Handles seed data loading from JSON or C# fallback

3. **Models** (`Models/`)
   - Domain entities (e.g., `Item` with Id, Name, Description, timestamps)

4. **DTOs** (`DTOs/`)
   - Data transfer objects for API requests (e.g., `CreateItemDto`, `UpdateItemDto`)
   - Include validation attributes

5. **Data** (`Data/`)
   - Seed data in two formats: `items-seed-data.json` (primary) and `ItemSeedData.cs` (fallback)
   - Service prioritizes JSON file, falls back to C# class

### Key Patterns

**In-Memory Storage**: All data is stored in `ItemService._items` (a `List<Item>`). Data is lost on restart and automatically reseeded.

**Singleton Service**: `ItemService` is registered as a singleton in dependency injection, maintaining state across requests within the application lifetime.

**Seed Data Strategy**: On startup, `ItemService` attempts to load from `Data/items-seed-data.json` first, falling back to `ItemSeedData.cs` if unavailable.

**Swagger Integration**: Uses Swashbuckle with XML comments and annotations for comprehensive API documentation. XML documentation file generation is enabled in `.csproj`.

**APIM-Ready**: The API is designed to work seamlessly with Azure API Management without code changes. See `APIM-INTEGRATION.md` for details.

## Development Guidelines

### Adding New Endpoints

When adding new controllers/endpoints:
1. Create controller in `Controllers/` inheriting from `ControllerBase`
2. Apply `[ApiController]`, `[Route]`, and `[Produces]` attributes
3. Use `[SwaggerTag]` and `[SwaggerOperation]` for documentation
4. Document response types with `[ProducesResponseType]`
5. Add XML comments (`/// <summary>`) for Swagger integration

### Modifying Data Models

- Update `Models/` for domain entities
- Create corresponding DTOs in `DTOs/` with validation attributes
- Update seed data in `Data/items-seed-data.json` and/or `ItemSeedData.cs`
- Consider impact on `ItemService` methods

### APIM Integration Notes

If deploying to Azure with APIM:
- No code changes required for basic integration
- OpenAPI spec available at `/swagger/v1/swagger.json` for APIM import
- Health endpoint (`/health`) works as-is for APIM backend probes
- Optional: Uncomment forwarded headers middleware in `Program.cs` to preserve client IPs
- Review CORS policy if restricting to APIM gateway only

### Swagger/OpenAPI

- Swagger UI is enabled in all environments at `/swagger`
- OpenAPI JSON spec at `/swagger/v1/swagger.json`
- XML comments from code are included in Swagger (requires `<GenerateDocumentationFile>true</GenerateDocumentationFile>`)
- Swagger annotations package (`Swashbuckle.AspNetCore.Annotations`) is used for enhanced documentation

## Configuration

### Application Settings
- `appsettings.json`: Main configuration
- `appsettings.Development.json`: Development overrides
- CORS is configured in `Program.cs` (currently allows all origins for development)

### Important Files
- `ApiProject.csproj`: Project configuration, NuGet packages, target framework (.NET 9)
- `Program.cs`: Application startup, service registration, middleware pipeline
- No solution file (`.sln`) exists; this is a single-project setup
