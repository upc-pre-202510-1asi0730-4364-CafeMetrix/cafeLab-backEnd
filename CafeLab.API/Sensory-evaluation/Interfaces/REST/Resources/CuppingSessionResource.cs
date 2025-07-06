using System;
using CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources
{
    public class CuppingSessionResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Origin { get; set; }
        public string Variety { get; set; }
        public string Process { get; set; }
        public string Lot { get; set; }
        public string Profile { get; set; }
        public int UserId { get; set; }
        public RatingsResource Ratings { get; set; }
    }
} 