using ServerCore.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerCore.Models.Database;

public class Challenge
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public Guid Id { get; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int Cost { get; set; }

    [Required]
    public Dictionary<CharacteristicType, int> Rewards { get; set; } = new();

    [Required]
    public List<Guid> UserIds { get; set; } = new();

    [Required]
    public List<UserGroups> UserGroups { get; set; } = new();

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