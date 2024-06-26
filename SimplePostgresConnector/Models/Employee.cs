﻿namespace SimplePostgresConnector.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Title { get; set; }
    public Company Company { get; set; } = null!;
}