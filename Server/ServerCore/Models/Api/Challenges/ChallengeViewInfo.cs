using ServerCore.Models.Database;
using ServerCore.Models.Enums;

namespace ServerCore.Models.Api.Challenges;

class ChallengeViewInfo
{
    public ChallengeViewInfo(Challenge value)
    {
        Id = value.Id;
        Name = value.Name;
        Cost = value.Cost;
        Description = value.Description;
        Rewards = value.Rewards;
    }

    public Guid Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public Dictionary<CharacteristicType, int> Rewards { get; set; }
}