using SimplePostgresConnector.Data;
using SimplePostgresConnector.Models;
using SimplePostgresConnector.Common;
using System.Threading;

namespace SimplePostgresConnector.Services
{
    public class DataService : IDataService
    {
        private readonly IAppDbContext _context;

        public DataService(IAppDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetEmployees() // may want canellationToken added
        {
            var employeesQueryable = _context.Employees.AsQueryable();

            // Skip this to return all employees
            //employeesQueryable = employeesQueryable.Where(x => x.Title == "Manager");

            return employeesQueryable.ToList();
        }

        // missing endpoint 1 GetEmployeesBasedOnTitle

        // missing endpoint 2 InsertEmployee

        public async Task<int> AddCompany(CancellationToken cancellationToken, string companyName, string address, DateTime openDate) 
        {
            var company = new Company
            {
                CompanyName = companyName,
                Address = address,
                OpenDate = Extensions.SetDateTimeKindToUtc(openDate)
            };

            _context.Companies.Add(company);

            try
            {
                await _context.SaveChangesAsync(cancellationToken); // cancellation token has nothing to do with the issue, does it?
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }

            return company.Id;
        }
    }
}
