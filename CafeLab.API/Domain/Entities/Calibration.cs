using CafeLab.API.Shared.Entities;
using System;

namespace CafeLab.API.Domain.Entities
{
    public class Calibration : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CalibrationDate { get; set; }
        public string Result { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
} 