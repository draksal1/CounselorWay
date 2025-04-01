using System;
using System.Collections.Generic;
using System.Linq;
using ServerCore.Models;
using ServerCore.Repositories.Contracts;

namespace ServerCore.Repositories
{
    public class InMemoryChallengeMapRepository : IChallengeMapRepository
    {
        private readonly Dictionary<Guid, ChallengeMap> _maps = new();

        public Guid Add(ChallengeMap map)
        {
            _maps[map.Id] = map;
            return map.Id;
        }

        public ChallengeMap Get(Guid id)
        {
            return _maps.TryGetValue(id, out var map) ? map : null;
        }

        public IEnumerable<ChallengeMap> GetBySeason(Guid seasonId)
        {
            return _maps.Values.Where(m => m.CampSeasonId == seasonId).ToList();
        }

        public bool Update(ChallengeMap map)
        {
            if (!_maps.ContainsKey(map.Id))
                return false;

            _maps[map.Id] = map;
            return true;
        }

        public bool Delete(Guid id)
        {
            return _maps.Remove(id);
        }

        public bool AddChallengeToMap(Guid mapId, Challenge challenge)
        {
            if (!_maps.TryGetValue(mapId, out var map))
                return false;

            map.Challenges.Add(challenge);
            return true;
        }
    }
}