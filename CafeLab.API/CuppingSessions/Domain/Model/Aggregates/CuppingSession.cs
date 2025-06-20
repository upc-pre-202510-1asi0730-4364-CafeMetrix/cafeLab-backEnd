using System;

namespace CafeLab.API.CuppingSessions.Domain.Model;

public class CuppingSession
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime Date { get; set; }
    public required string Origin { get; set; }
    public required string Variety { get; set; }
    public required string Process { get; set; }
    public required string Lot { get; set; }
    public required string Profile { get; set; }
    public required Ratings Ratings { get; set; }

    public CuppingSession(
        int id, string name, DateTime date, string origin, string variety, 
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