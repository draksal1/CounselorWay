using System;
using System.Collections.Generic;
using System.Linq;
using ServerCore.Models;
using ServerCore.Repositories;
using ServerCore.Services.Contracts;
using ServerCore.DTOs;
using ServerCore.Repositories.Contracts;

namespace ServerCore.Services.Implementations
{
    public class CampSeasonService : ICampSeasonService
    {
        private readonly ICampSeasonRepository _seasonRepo;
        private readonly IChallengeMapRepository _mapRepo;
        private readonly IUserRepository _userRepo;

        public CampSeasonService(
            ICampSeasonRepository seasonRepo,
            IChallengeMapRepository mapRepo,
            IUserRepository userRepo)
        {
            _seasonRepo = seasonRepo;
            _mapRepo = mapRepo;
            _userRepo = userRepo;
        }

        public Guid CreateSeason(string name, DateTime startDate, DateTime endDate)
        {
            var season = new CampSeason(name, startDate, endDate);
            return _seasonRepo.Add(season);
        }

        public bool AddUserToSeason(Guid seasonId, Guid userId)
        {
            return _seasonRepo.AddUserToSeason(seasonId, userId);
        }

        public IEnumerable<ChallengeMap> GetSeasonMaps(Guid seasonId)
        {
            // Получаем все карты для указанного сезона
            return _mapRepo.GetBySeason(seasonId);
        }

        public Guid CreateChallengeMap(Guid seasonId, int weekNumber, string mapName)
        {
            var map = new ChallengeMap(seasonId, weekNumber, mapName);
            var mapId = _mapRepo.Add(map);
            _seasonRepo.AddMapToSeason(seasonId, mapId);
            return mapId;
        }

        public bool AddLocationChallengeToMap(Guid mapId, Challenge challenge)
        {
            return _mapRepo.AddChallengeToMap(mapId, challenge);
        }

        public IEnumerable<CampSeason> GetAllSeasons()
        {
            return _seasonRepo.GetAll();
        }

        public CampSeason GetSeason(Guid id)
        {
            return _seasonRepo.Get(id);
        }

        public ChallengeMap GetChallengeMap(Guid id)
        {
            return _mapRepo.Get(id);
        }

        public Guid UploadChallengeMap(Guid seasonId, ChallengeMapDto mapDto)
        {
            // 1. Получаем сезон
            var season = _seasonRepo.Get(seasonId);
            if (season == null)
                throw new ArgumentException("Сезон не найден");

            // 2. Создаем карту заданий
            var map = new ChallengeMap(seasonId, mapDto.WeekNumber, mapDto.Name);

            // 3. Добавляем задания на карту
            foreach (var challengeDto in mapDto.Challenges)
            {
                var challenge = new Challenge(
                    challengeDto.Title,
                    challengeDto.Description,
                    challengeDto.Cost,
                    challengeDto.LocationId);

                foreach (var reward in challengeDto.Rewards)
                {
                    challenge.AddReward(reward.Key, reward.Value);
                }

                map.Challenges.Add(challenge);
            }

            // 4. Сохраняем карту
            var mapId = _mapRepo.Add(map);
            _seasonRepo.AddMapToSeason(seasonId, mapId);

            // 5. Назначаем задания всем пользователям сезона
            foreach (var userId in season.UserIds)
            {
                var user = _userRepo.Get(userId);
                if (user != null)
                {
                    if (user.CompletedMapChallenges == null)
                        user.CompletedMapChallenges = new Dictionary<Guid, List<Guid>>();

                    if (!user.CompletedMapChallenges.ContainsKey(mapId))
                        user.CompletedMapChallenges[mapId] = new List<Guid>();

                    _userRepo.Update(user);
                }
            }

            return mapId;
        }
    }
}