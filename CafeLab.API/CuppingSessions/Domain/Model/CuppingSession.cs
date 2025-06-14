using System;

namespace CafeLab.API.CuppingSessions.Domain.Model;

public class CuppingSession
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public string Origin { get; set; }
    public string Variety { get; set; }
    public string Process { get; set; }
    public string Lot { get; set; }
    public string Profile { get; set; }
    public Ratings Ratings { get; set; }

    public CuppingSession(
        int id, string name, DateOnly date, string origin, string variety, 
        string process, string lot, string profile, Ratings ratings)
    {
        Id = id;
        Name = name;
        Date = date;
        Origin = origin;
        Variety = variety;
        Process = process;
        Lot = lot;
        Profile = profile;
        Ratings = ratings;
    }

    // Constructor sin parámetros para Entity Framework Core
    public CuppingSession() { }
} 