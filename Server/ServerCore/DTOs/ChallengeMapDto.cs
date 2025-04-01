using System;
using System.Collections.Generic;

namespace ServerCore.DTOs
{
    public class ChallengeMapDto
    {
        public int WeekNumber { get; set; }
        public string Name { get; set; }
        public List<ChallengeDto> Challenges { get; set; } = new List<ChallengeDto>();
    }
}