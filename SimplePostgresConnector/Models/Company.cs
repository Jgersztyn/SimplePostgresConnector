namespace SimplePostgresConnector.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public ICollection<Employee> Employee { get; set; } = null!;
    }
}
