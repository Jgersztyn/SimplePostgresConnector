using System;
using System.ComponentModel.Design;
using System.Xml.Linq;
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

            // why does database connection time out...?
            // TODO: Investigate why the connection times out
            conn.Open();

            // Query the Employees based on title
            string sql = "SELECT * FROM public.\"Employees\" WHERE \"Title\" != @value";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                // Replace @value with the actual parameter value
                cmd.Parameters.AddWithValue("value", title);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Title = reader.GetString(2),
                            Company = new Company
                            {
                                Id = reader.GetInt32(3)
                            }
                        };

                        resultList.Add(employee);
                    }
                }
            }
        }

        return resultList;
    }
}
