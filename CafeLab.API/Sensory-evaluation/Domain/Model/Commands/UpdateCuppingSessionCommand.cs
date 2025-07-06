using System;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;

namespace CafeLab.API.Sensory_evaluation.Domain.Model.Commands
{
    public class UpdateCuppingSessionCommand
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public string Origin { get; }
        public string Variety { get; }
        public string Process { get; }
        public string Lot { get; }
        public string Profile { get; }
        public CuppingSessionRatings Ratings { get; }
        public int UserId { get; }

        public UpdateCuppingSessionCommand(int id, string name, DateTime date, string origin, string variety, string process, string lot, string profile, CuppingSessionRatings ratings, int userId)
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
            UserId = userId;
        }
    }
} 