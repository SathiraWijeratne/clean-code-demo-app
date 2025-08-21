using Application.Commands;
using Application.Queries;
using Infrastructure.DependencyInjection;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// add infrastructure services (includes both application services and MediatR)
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// User endpoints using MediatR
app.MapGet("/users", async (IMediator mediator) =>
{
    return await mediator.Send(new GetAllUsersQuery());
});

app.MapGet("/users/{id:int}", async (int id, IMediator mediator) =>
{
    return await mediator.Send(new GetUserByIdQuery { Id = id });
});

app.MapPost("/users", async (CreateUserCommand command, IMediator mediator) =>
{
    await mediator.Send(command);
    return Results.Created();
});

app.MapPut("/users/{id:int}", async (int id, UpdateUserCommand command, IMediator mediator) =>
{
    command.Id = id;
    await mediator.Send(command);
    return Results.NoContent();
});

app.MapDelete("/users/{id:int}", async (int id, IMediator mediator) =>
{
    await mediator.Send(new DeleteUserCommand { Id = id });
    return Results.NoContent();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
