using SimplePostgresConnector;
using SimplePostgresConnector.Data;
using SimplePostgresConnector.Models;
using SimplePostgresConnector.Persistence;
using SimplePostgresConnector.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.Local.json", true, true).Build();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// for using the Endpoints.cs logic
//app.MapEndpoints();

app.UseHttpsRedirection();

// Call the get-employees endpoint the old way
// for testing
//app.MapGet("/get-employees", () =>
//{
//    var employees = DatabaseCalls.GetEmployees();

//    return employees;
//})
//.WithName("GetEmployees")
//.WithOpenApi();

// Call the endpoint the new way!
// with DBContext involved
app.MapGet("/get-employees", async (IDataService dataService) => // (IAppDbContext context) =>
{
    // directly accessing the employees by using the context
    //var employees = context.Employees.ToList();

    // using the data service to get the employees
    var employees = dataService.GetEmployees();

    return employees;
})
.WithName("GetEmployees")
.WithOpenApi();

app.MapGet("/get-employees/{title}", async (string title) =>
{
    var employees = DatabaseCalls.GetEmployeesBasedOnTitle(title);

    return employees;
})
.WithName("GetEmployees/{title}")
.WithOpenApi();

app.MapPost("/add-employee", async (string employeeName, string employeeTitle, int companyId) =>
{
    DatabaseCalls.InsertEmployee(employeeName, employeeTitle, companyId);

    // return the newly created employee
    return Results.Created("/get-employees", null);
})
.WithName("AddEmployee")
.WithOpenApi();

// Call the add-company endpoint the old way
// for testing
//app.MapPost("/add-company", (string companyName, string address, DateTime openDate) =>
//{
//    DatabaseCalls.InsertCompany(companyName, address, openDate);

//    // return the newly created company
//    return Results.Created("/add-company", null);
//})
//.WithName("AddCompany")
//.WithOpenApi();

// Call the endpoint the new way!
// with DBContext involved
app.MapPost("/add-company", async (IDataService dataservice, CancellationToken cancellationToken, string companyName, string address, DateTime openDate) =>
{
    await dataservice.AddCompany(cancellationToken, companyName, address, openDate);

    // return the newly created company
    return Results.Created("/add-company", null);
})
.WithName("AddCompany")
.WithOpenApi();

app.Run();