using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimplePostgresConnector.Models;

namespace SimplePostgresConnector.Data;

public class AppDbContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Configure the dbcontext
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresConnection"));
    }

    public DbSet<Employee> Employees { get; set; }

    // for simplicity sake, we are assuming only one Employee per Company
    public DbSet<Company> Companies { get; set; }
}
