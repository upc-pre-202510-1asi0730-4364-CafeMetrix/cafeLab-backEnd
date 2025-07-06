using System.Collections.Generic;

namespace CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;

public class Supplier
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Location { get; private set; }
    public List<string> Specialties { get; private set; }
    public int UserId { get; private set; } // Relación con Profile

    public Supplier()
    {
        Specialties = new List<string>();
    }
    
    public Supplier(string name, string email, string phone, string location, List<string> specialties, int userId)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Location = location;
        Specialties = specialties ?? new List<string>();
        UserId = userId;
    }

    public void Update(string name, string email, string phone, string location, List<string> specialties)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Location = location;
        Specialties = specialties ?? new List<string>();
    }
} 