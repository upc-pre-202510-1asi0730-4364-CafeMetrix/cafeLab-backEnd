using System;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources
{
    public class BaristaCuppingSessionResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Profile { get; set; }
        public int UserId { get; set; }
    }
} 