using ServerCore.Models.Enums;

namespace ServerCore.Models.Database;

public class Characteristics
{
    public Dictionary<CharacteristicType, int> Stats { get; } = new()
    {
        { CharacteristicType.Strength, 10 },
        { CharacteristicType.Agility, 10 },
        { CharacteristicType.Intelligence, 10 },
        { CharacteristicType.Stamina, 10 },
        { CharacteristicType.Luck, 5 }
    };

    public void IncreaseStat(CharacteristicType type, int value)
    {
        Stats[type] += value;
    }

    public int GetStat(CharacteristicType type) => Stats[type];
}