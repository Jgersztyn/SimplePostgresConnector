using SimplePostgresConnector.Models;
using SimplePostgresConnector.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/get-weather", () =>
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
.WithName("GetWeather")
.WithOpenApi();

app.MapGet("/get-employees/{title}", (string title) =>
{
    // Format user input for Postgres
    var formattedTitle = string.Format($"\"{title}\"");
    var employees = DatabaseCalls.GetEmployeesBasedOnTitle(formattedTitle);

    return employees;
})
.WithName("GetEmployees")
.WithOpenApi();

app.MapPost("/add-employee", (string employeeName, string employeeTitle, int companyId) =>
{
    DatabaseCalls.InsertEmployee(employeeName, employeeTitle, companyId);

    // return the newly created employee
    return Results.Created("/get-employees", null);
})
.WithName("AddEmployee")
.WithOpenApi();

app.MapPost("/add-company", (string companyName, string address, DateTime openDate) =>
{
    DatabaseCalls.InsertCompany(companyName, address, openDate);

    // return the newly created company
    return Results.Created("/add-company", null);
})
.WithName("AddCompany")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
