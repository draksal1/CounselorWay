using System;
using System.Collections.Generic;
using System.Linq;
using ServerCore.Models;
using ServerCore.Repositories;
using ServerCore.Repositories.Contracts;
using ServerCore.Services.Contracts;

namespace ServerCore.Services.Implementations
{
    public class ChallengeService : IChallengeService
    {
        private readonly IUserRepository _userRepo;
        private readonly IChallengeMapRepository _mapRepo;
        private readonly ICampSeasonRepository _seasonRepo;

        public ChallengeService(
            IUserRepository userRepo,
            IChallengeMapRepository mapRepo,
            ICampSeasonRepository seasonRepo)
        {
            _userRepo = userRepo;
            _mapRepo = mapRepo;
            _seasonRepo = seasonRepo;
        }

        public bool AssignPersonalChallenge(Guid userId, Challenge challenge)
        {
            var user = _userRepo.Get(userId);
            if (user == null) return false;

            if (user.PersonalChallenges == null)
                user.PersonalChallenges = new Dictionary<Guid, bool>();

            user.PersonalChallenges[challenge.Id] = false;
            return _userRepo.Update(user);
        }

        public bool CompleteLocationChallenge(Guid userId, Guid mapId, Guid challengeId)
        {
            var user = _userRepo.Get(userId);
            if (user == null) return false;

            if (user.CompletedMapChallenges == null)
                user.CompletedMapChallenges = new Dictionary<Guid, List<Guid>>();

            if (!user.CompletedMapChallenges.ContainsKey(mapId))
                user.CompletedMapChallenges[mapId] = new List<Guid>();

            user.CompletedMapChallenges[mapId].Add(challengeId);
            return _userRepo.Update(user);
        }

        public bool CompletePersonalChallenge(Guid userId, Guid challengeId)
        {
            var user = _userRepo.Get(userId);
            if (user == null || user.PersonalChallenges == null)
                return false;

            if (user.PersonalChallenges.ContainsKey(challengeId))
            {
                user.PersonalChallenges[challengeId] = true;
                return _userRepo.Update(user);
            }
            return false;
        }

        public IEnumerable<Challenge> GetAvailableChallenges(Guid userId, Guid seasonId)
        {
            var user = _userRepo.Get(userId);
            if (user == null)
                return Enumerable.Empty<Challenge>();

            var season = _seasonRepo.Get(seasonId);
            if (season == null || season.ChallengeMapIds == null)
                return Enumerable.Empty<Challenge>();

            var availableChallenges = new List<Challenge>();

            // Обрабатываем карточные задания
            foreach (var mapId in season.ChallengeMapIds)
            {
                var map = _mapRepo.Get(mapId);
                if (map?.Challenges == null) continue;

                var completedChallenges = user.CompletedMapChallenges != null &&
                                        user.CompletedMapChallenges.TryGetValue(mapId, out var completed)
                    ? completed
                    : new List<Guid>();

                availableChallenges.AddRange(
                    map.Challenges.Where(c => !completedChallenges.Contains(c.Id)));
            }

            // Обрабатываем персональные задания
            if (user.PersonalChallenges != null)
            {
                var incompletePersonal = user.PersonalChallenges
                    .Where(p => !p.Value)
                    .Select(p => new Challenge("Personal", "Description", 0) { Id = p.Key });

                availableChallenges.AddRange(incompletePersonal);
            }

            return availableChallenges;
        }
    }
}