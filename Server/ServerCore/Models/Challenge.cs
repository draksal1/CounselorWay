using ServerCore.Models.Enums;

namespace ServerCore.Models
{
    public class Challenge
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public Dictionary<CharacteristicType, int> Rewards { get; set; } = new();

        public Challenge(string name, string description, int cost)
        {
            Name = name;
            Description = description;
            Cost = cost;
        }

        public void AddReward(CharacteristicType type, int value)
        {
            Rewards[type] = value;
        }
    }
}