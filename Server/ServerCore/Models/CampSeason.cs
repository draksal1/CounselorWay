using System;
using System.Collections.Generic;

namespace ServerCore.Models
{
    public class CampSeason
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> ChallengeMapIds { get; set; } = new List<Guid>();
        public List<Guid> UserIds { get; set; } = new List<Guid>();

        public CampSeason(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}