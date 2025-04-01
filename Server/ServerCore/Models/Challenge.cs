using ServerCore.Models.Enums;

namespace ServerCore.Models
{
    public class Challenge
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public Dictionary<CharacteristicType, int> Rewards { get; set; } = new();
        public Guid? LocationId { get; set; } // null для персональных заданий

        public Challenge(string title, string description, int cost, Guid? locationId = null)
        {
            Title = title;
            Description = description;
            Cost = cost;
            LocationId = locationId;
        }

        public void AddReward(CharacteristicType type, int value)
        {
            Rewards[type] = value;
        }
    }
}