using System;
using System.Text.Json.Serialization;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources
{
    public class CreateBaristaCuppingSessionResource
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Profile { get; set; }
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
    }
} 