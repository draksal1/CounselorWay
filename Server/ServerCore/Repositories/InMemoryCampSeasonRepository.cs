using System;
using System.Collections.Generic;
using System.Linq;
using ServerCore.Models;
using ServerCore.Repositories.Contracts;

namespace ServerCore.Repositories.Implementations
{
    public class InMemoryCampSeasonRepository : ICampSeasonRepository
    {
        private readonly Dictionary<Guid, CampSeason> _seasons = new();

        public Guid Add(CampSeason season)
        {
            _seasons[season.Id] = season;
            return season.Id;
        }

        public CampSeason Get(Guid id)
        {
            return _seasons.TryGetValue(id, out var season) ? season : null;
        }

        public IEnumerable<CampSeason> GetAll()
        {
            return _seasons.Values.ToList();
        }

        public bool Update(CampSeason season)
        {
            if (!_seasons.ContainsKey(season.Id))
                return false;

            _seasons[season.Id] = season;
            return true;
        }

        public bool Delete(Guid id)
        {
            return _seasons.Remove(id);
        }

        public bool AddMapToSeason(Guid seasonId, Guid mapId)
        {
            if (!_seasons.TryGetValue(seasonId, out var season))
                return false;

            season.ChallengeMapIds.Add(mapId);
            return true;
        }

        public bool AddUserToSeason(Guid seasonId, Guid userId)
        {
            if (!_seasons.TryGetValue(seasonId, out var season))
                return false;

            season.UserIds.Add(userId);
            return true;
        }
    }
}