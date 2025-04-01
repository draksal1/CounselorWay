using ServerCore.Models;

namespace ServerCore.Repositories.Contracts
{
    public interface ICampSeasonRepository
    {
        Guid Add(CampSeason season);
        CampSeason Get(Guid id);
        IEnumerable<CampSeason> GetAll();
        bool Update(CampSeason season);
        bool Delete(Guid id);
        bool AddMapToSeason(Guid seasonId, Guid mapId);
        bool AddUserToSeason(Guid seasonId, Guid userId);
    }
}
