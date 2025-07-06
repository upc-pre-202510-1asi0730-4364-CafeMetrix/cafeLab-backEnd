using System;
using CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources;
using System.Text.Json.Serialization;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources
{
    public class CreateCuppingSessionResource
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Origin { get; set; }
        public string Variety { get; set; }
        public string Process { get; set; }
        public string Lot { get; set; }
        public string Profile { get; set; }
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        public RatingsResource Ratings { get; set; }
    }
} 