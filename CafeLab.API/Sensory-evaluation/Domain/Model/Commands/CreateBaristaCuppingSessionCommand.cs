using System;

namespace CafeLab.API.Sensory_evaluation.Domain.Model.Commands
{
    public class CreateBaristaCuppingSessionCommand
    {
        public string Name { get; }
        public DateTime Date { get; }
        public string Profile { get; }
        public int UserId { get; }

        public CreateBaristaCuppingSessionCommand(string name, DateTime date, string profile, int userId)
        {
            Name = name;
            Date = date;
            Profile = profile;
            UserId = userId;
        }
    }
} 