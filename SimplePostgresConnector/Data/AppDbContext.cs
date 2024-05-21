using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimplePostgresConnector.Models; // Should rename Models to Entities?
using System.Reflection;

namespace SimplePostgresConnector.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
