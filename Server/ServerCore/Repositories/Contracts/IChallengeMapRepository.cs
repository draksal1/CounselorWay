using ServerCore.Models;

namespace ServerCore.Repositories
{
    public interface IChallengeMapRepository
    {
        Guid Add(ChallengeMap map);
        ChallengeMap Get(Guid id);
        IEnumerable<ChallengeMap> GetBySeason(Guid seasonId);
        bool Update(ChallengeMap map);
        bool Delete(Guid id);
        bool AddChallengeToMap(Guid mapId, Challenge challenge);
    }
}