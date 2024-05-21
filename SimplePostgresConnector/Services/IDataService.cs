using SimplePostgresConnector.Models;

namespace SimplePostgresConnector.Services
{
    public interface IDataService
    {
        public List<Employee> GetEmployees();

        // missing endpoint 1 GetEmployeesBasedOnTitle

        // missing endpoint 2 InsertEmployee

        public Task<int> AddCompany(CancellationToken cancellationToken, string companyName, string address, DateTime openDate);
    }
}
