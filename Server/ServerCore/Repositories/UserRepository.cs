using System;
using System.Collections.Generic;
using ServerCore.Models;

namespace ServerCore.Repositories
{
    public class UserRepository
    {
        private readonly Dictionary<Guid, User> _users = new();

        public Guid AddUser(User user)
        {
            _users[user.Id] = user;
            return user.Id;
        }

        public User GetUser(Guid id)
        {
            return _users.TryGetValue(id, out var user) ? user : null;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users.Values; // Уже возвращает read-only коллекцию
        }

        public bool RemoveUser(Guid id)
        {
            return _users.Remove(id);
        }

        public bool AssignGeneralChallengeToUser(Guid userId, Guid challengeId)
        {
            if (!_users.TryGetValue(userId, out var user))
                return false;

            user.AddGeneralChallenge(challengeId);
            return true;
        }
    }
}