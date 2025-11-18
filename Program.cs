using ApiProject.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddSingleton<ItemService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Items API",
        Description = "A simple RESTful API for managing items. Built with .NET 9 for learning and experimentation purposes.",
        Contact = new OpenApiContact
        {
            Name = "API Support"
        }
    });

    // Include XML comments for better documentation
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    // Enable annotations for better Swagger documentation
    options.EnableAnnotations();
});

// Configure CORS for development
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Optional: Forwarded Headers for APIM integration
// Uncomment if you need to preserve original client IP when behind APIM
// builder.Services.Configure<ForwardedHeadersOptions>(options =>
// {
//     options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
//                                 Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
//     options.KnownNetworks.Clear();
//     options.KnownProxies.Clear();
// });

var app = builder.Build();

// Configure the HTTP request pipeline

// Optional: Use forwarded headers (uncomment if enabled above)
// app.UseForwardedHeaders();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Items API v1");
    options.RoutePrefix = "swagger"; // Swagger UI will be available at /swagger
    options.DisplayRequestDuration();
});

app.UseCors();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

