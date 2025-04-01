using System.Collections.Generic;
using ServerCore.Models;
using ServerCore.Models.Enums;

namespace ServerCore.DTOs
{
    public class ChallengeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public Dictionary<CharacteristicType, int> Rewards { get; set; }
        public Guid? LocationId { get; set; }
        public bool IsCompleted { get; set; }

        public static ChallengeDto FromChallenge(Challenge challenge, bool isCompleted = false) => new()
        {
            Id = challenge.Id,
            Title = challenge.Title,
            Description = challenge.Description,
            Cost = challenge.Cost,
            Rewards = challenge.Rewards,
            LocationId = challenge.LocationId,
            IsCompleted = isCompleted
        };
    }
}