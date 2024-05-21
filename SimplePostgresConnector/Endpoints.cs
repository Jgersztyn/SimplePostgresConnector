using SimplePostgresConnector.Models;

namespace SimplePostgresConnector;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        //app.MapPost("/post-ncep-data", Functions.PostNCEPDataRequest);

        //app.MapGet("/companies", async (IAppDbContext dbContext) =>
        //{
        //    return Results.Ok(await dbContext.Companies.ToListAsync());
        //});


        return app;
    }
}