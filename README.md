# .NET 9 RESTful API Project

A simple RESTful API project built with .NET 9 for learning and experimentation purposes. No authentication required.

## Setup

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed on your machine

### Installation

1. Clone or navigate to this project directory
2. Restore dependencies:
```bash
dotnet restore
```

3. Run the project:
```bash
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

Swagger UI will be available at:
- `https://localhost:5001/swagger` - Interactive API documentation and testing interface
- `https://localhost:5001/swagger/v1/swagger.json` - OpenAPI JSON specification

## Usage

### API Endpoints

#### Health Check
- **GET** `/health` - Check API health status

#### Items API

All endpoints are prefixed with `/api/items`

- **GET** `/api/items` - Get all items
- **GET** `/api/items/{id}` - Get item by ID
- **POST** `/api/items` - Create a new item
- **PUT** `/api/items/{id}` - Update an existing item
- **DELETE** `/api/items/{id}` - Delete an item

### Example Requests

#### Create an Item
```bash
curl -X POST https://localhost:5001/api/items \
  -H "Content-Type: application/json" \
  -d '{
    "name": "My First Item",
    "description": "This is a test item"
  }'
```

#### Get All Items
```bash
curl https://localhost:5001/api/items
```

#### Get Item by ID
```bash
curl https://localhost:5001/api/items/1
```

#### Update an Item
```bash
curl -X PUT https://localhost:5001/api/items/1 \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Updated Item Name",
    "description": "Updated description"
  }'
```

#### Delete an Item
```bash
curl -X DELETE https://localhost:5001/api/items/1
```

## Stack

- **.NET 9** - Framework
- **ASP.NET Core Controllers** - MVC controller-based API endpoints
- **Swagger/OpenAPI** - Interactive API documentation and testing
- **In-Memory Data Store** - Simple list-based storage (data is lost on restart)

## Swagger Documentation

The API includes comprehensive Swagger/OpenAPI documentation with the following features:

- **Interactive UI** - Test all endpoints directly from the browser at `/swagger`
- **Detailed Documentation** - XML comments and annotations provide rich descriptions for each endpoint
- **Request/Response Examples** - See example request and response formats
- **Try It Out** - Execute API calls directly from the Swagger UI
- **OpenAPI Specification** - Access the JSON specification at `/swagger/v1/swagger.json`

### Swagger Features

- All endpoints are documented with summaries and descriptions
- Request parameters and response types are clearly defined
- HTTP status codes are documented for each endpoint
- Tags organize endpoints by functionality (Items, Health)
- Request duration is displayed in the Swagger UI

## Project Structure

```
.
├── Controllers/       # API Controllers
│   ├── ItemsController.cs
│   └── HealthController.cs
├── Data/              # Seed data files
│   ├── items-seed-data.json  # JSON seed data
│   └── ItemSeedData.cs       # C# seed data class
├── DTOs/              # Data Transfer Objects
│   ├── CreateItemDto.cs
│   └── UpdateItemDto.cs
├── Models/            # Domain models
│   └── Item.cs
├── Services/          # Business logic
│   └── ItemService.cs
├── Program.cs         # Application entry point and API configuration
├── appsettings.json   # Application settings
└── ApiProject.csproj  # Project file
```

## Seed Data

The API comes pre-loaded with dummy data for testing purposes. The `ItemService` automatically loads seed data on startup:

1. **JSON File** (`Data/items-seed-data.json`) - Primary source, loaded first if available
2. **C# Class** (`Data/ItemSeedData.cs`) - Fallback seed data if JSON file is not found

The seed data includes 5 sample items (Laptop, Wireless Mouse, Mechanical Keyboard, Monitor, USB-C Hub) that will be available immediately when you start the API.

You can modify either file to customize the initial data:
- Edit `items-seed-data.json` for easy JSON-based updates
- Edit `ItemSeedData.cs` for type-safe C# seed data

## Azure API Management (APIM) Integration

This API is **ready for Azure API Management integration** without any code changes required!

- ✅ No code changes needed for basic APIM integration
- ✅ Swagger endpoint can be imported directly into APIM
- ✅ Health check endpoint (`/health`) works perfectly for APIM health probes
- ✅ All RESTful endpoints work seamlessly behind APIM

See [APIM-INTEGRATION.md](./APIM-INTEGRATION.md) for detailed integration guide and optional enhancements.

## Notes

- This is a play-around project with no authentication
- Data is stored in-memory and will be lost when the application restarts
- Seed data is automatically loaded on application startup
- CORS is enabled for all origins (development only)
- Swagger UI is available in all environments for easy API testing and documentation

