using Npgsql;
using SimplePostgresConnector.Models;

namespace SimplePostgresConnector.Persistence;

public static class DatabaseCalls
{
    public static List<Employee> GetEmployeesBasedOnTitle(string title)
    {
        // Obviously not best practice, but these database calls are for demonstration
        const string connectionString = "";

        List<Employee> resultList = new List<Employee>();

        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();

            // Query the Employees based on title
            string sql = "SELECT public.\"Employees\".\"Id\", public.\"Employees\".\"Name\", public.\"Employees\".\"Title\" FROM public.\"Employees\"" +
                         "INNER JOIN public.\"Companies\" ON public.\"Employees\".\"CompanyId\" = public.\"Companies\".\"Id\"" +
                         "WHERE \"Title\" != @title";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                // Replace @title with the actual parameter value
                cmd.Parameters.AddWithValue("title", title);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var name = reader.GetString(1);
                        var employeeTitle = reader.GetString(2);

                        Employee employee = new Employee
                        {
                            Id = id,
                            Name = name,
                            Title = employeeTitle,
                            Company = new Company()
                        };

                        resultList.Add(employee);
                    }
                }
            }
        }

        return resultList;
    }

    public static void InsertEmployee(string name, string title, int companyId)
    {
        const string connectionString = "";

        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();

            string sql = "INSERT INTO public.\"Employees\" (\"Name\", \"Title\", \"CompanyId\") VALUES (@name, @title, @companyId)";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("title", title);
                cmd.Parameters.AddWithValue("companyId", companyId);

                // Execute the query
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) inserted.");
            }
        }
    }

    public static void InsertCompany(string companyName, string address, DateTime openDate, DateTime? closeDate = null)
    {
        const string connectionString = "";

        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();

            string sql = "INSERT INTO public.\"Companies\" (\"CompanyName\", \"Address\", \"OpenDate\", \"CloseDate\") VALUES (@companyName, @address, @openDate, @closeDate)";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("companyName", companyName);
                cmd.Parameters.AddWithValue("address", address);
                cmd.Parameters.AddWithValue("openDate", openDate);
                cmd.Parameters.AddWithValue("closeDate", closeDate != null ? closeDate : DBNull.Value);

                // Execute the query
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) inserted.");
            }
        }
    }
}
