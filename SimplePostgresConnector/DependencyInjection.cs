using Microsoft.EntityFrameworkCore.Diagnostics;
//using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using SimplePostgresConnector.Data;
using Microsoft.EntityFrameworkCore;
using SimplePostgresConnector.Services;

namespace SimplePostgresConnector;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");

        //var config = new Config();
        //configuration.Bind("Config", config);
        //services.AddSingleton(config);

        services.AddScoped<IDataService, DataService>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            //options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
            //options.UseLowerCaseNamingConvention();
        });

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        //services.AddScoped<AppDbContextInitializer>();

        return services;
    }
}