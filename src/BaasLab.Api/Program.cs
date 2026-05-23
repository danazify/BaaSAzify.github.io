using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () =>
{
    var response = new
    {
        service = "BaasLab.Api",
        status = "Healthy",
        environment = app.Environment.EnvironmentName,
        version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
        timestampUtc = DateTimeOffset.UtcNow
    };

    return Results.Ok(response);
})
.WithName("HealthCheck")
.WithTags("Health");

app.Run();