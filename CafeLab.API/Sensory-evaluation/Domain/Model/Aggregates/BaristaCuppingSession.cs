using System;

namespace CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates
{
    public class BaristaCuppingSession
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public string Profile { get; private set; }
        public int UserId { get; private set; }

        public BaristaCuppingSession(string name, DateTime date, string profile, int userId)
        {
            Name = name;
            Date = date;
            Profile = profile;
            UserId = userId;
        }

        public void Update(string name, DateTime date, string profile)
        {
            Name = name;
            Date = date;
            Profile = profile;
        }
    }
} 