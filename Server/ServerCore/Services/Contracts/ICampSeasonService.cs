using ServerCore.Models;

namespace ServerCore.Services.Contracts
{
    public interface ICampSeasonService
    {
        Guid CreateSeason(string name, DateTime startDate, DateTime endDate);
        Guid CreateChallengeMap(Guid seasonId, int weekNumber, string mapName);
        bool AddLocationChallengeToMap(Guid mapId, Challenge challenge);
        CampSeason GetSeason(Guid id);
        ChallengeMap GetChallengeMap(Guid id);
    }
}