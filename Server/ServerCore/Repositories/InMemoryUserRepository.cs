using System;
using System.Collections.Generic;
using System.Linq;
using ServerCore.Models;
using ServerCore.Repositories.Contracts;

namespace ServerCore.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly Dictionary<Guid, User> _users = new();

        public Guid Add(User user)
        {
            _users[user.Id] = user;
            return user.Id;
        }

        public User Get(Guid id)
        {
            return _users.TryGetValue(id, out var user) ? user : null;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Values.ToList();
        }

        public bool Update(User user)
        {
            if (!_users.ContainsKey(user.Id))
                return false;

            _users[user.Id] = user;
            return true;
        }

        public bool Delete(Guid id)
        {
            return _users.Remove(id);
        }

        public bool AssignPersonalChallenge(Guid userId, Guid challengeId)
        {
            if (!_users.TryGetValue(userId, out var user))
                return false;

            user.PersonalChallenges[challengeId] = false;
            return true;
        }

        public bool CompleteMapChallenge(Guid userId, Guid mapId, Guid challengeId)
        {
            if (!_users.TryGetValue(userId, out var user))
                return false;

            if (!user.CompletedMapChallenges.ContainsKey(mapId))
                user.CompletedMapChallenges[mapId] = new List<Guid>();

            user.CompletedMapChallenges[mapId].Add(challengeId);
            return true;
        }

        public bool CompletePersonalChallenge(Guid userId, Guid challengeId)
        {
            if (!_users.TryGetValue(userId, out var user))
                return false;

            if (user.PersonalChallenges.ContainsKey(challengeId))
            {
                user.PersonalChallenges[challengeId] = true;
                return true;
            }
            return false;
        }
    }
}