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

app.MapGet("/get-employees", () =>
{
    var employees = DatabaseCalls.GetEmployees();

    return employees;
})
.WithName("GetEmployees")
.WithOpenApi();

app.MapGet("/get-employees/{title}", (string title) =>
{
    var employees = DatabaseCalls.GetEmployeesBasedOnTitle(title);

    return employees;
})
.WithName("GetEmployees/{title}")
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