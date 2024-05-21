using Microsoft.EntityFrameworkCore;
using SimplePostgresConnector.Models;

namespace SimplePostgresConnector.Data;

public interface IAppDbContext
{
    public DbSet<Company> Companies { get; set; }

    public DbSet<Employee> Employees { get; set; }

    // does it matter if this is public or not?
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
