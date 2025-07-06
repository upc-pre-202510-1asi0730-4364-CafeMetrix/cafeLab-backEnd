using System;

namespace CafeLab.API.Sensory_evaluation.Domain.Model.Commands
{
    public class UpdateBaristaCuppingSessionCommand
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public string Profile { get; }
        public int UserId { get; }

        public UpdateBaristaCuppingSessionCommand(int id, string name, DateTime date, string profile, int userId)
        {
            Id = id;
            Name = name;
            Date = date;
            Profile = profile;
            UserId = userId;
        }
    }
} 