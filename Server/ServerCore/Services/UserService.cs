using System;
using ServerCore.Models;
using ServerCore.Repositories;
using ServerCore.Repositories.Contracts;
using ServerCore.Services.Contracts;
using ServerCore.DTOs;

namespace ServerCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public Guid RegisterUser(string name, int age)
        {
            var user = new User(name, age);
            return _userRepo.Add(user);
        }

        public User GetUser(Guid id)
        {
            return _userRepo.Get(id);
        }

        public bool UpdateUser(Guid userId, Action<User> updateAction)
        {
            var user = _userRepo.Get(userId);
            if (user == null)
                return false;

            updateAction(user);
            return _userRepo.Update(user);
        }

        public bool AssignPersonalChallenge(Guid userId, Challenge challenge)
        {
            var user = _userRepo.Get(userId);
            if (user == null)
                return false;

            if (user.PersonalChallenges == null)
                user.PersonalChallenges = new Dictionary<Guid, bool>();

            user.PersonalChallenges[challenge.Id] = false;
            return _userRepo.Update(user);
        }

        public bool UpdateCharacteristics(Guid userId, Action<Characteristics> updateAction)
        {
            var user = _userRepo.Get(userId);
            if (user == null)
                return false;

            updateAction(user.Characteristics);
            return _userRepo.Update(user);
        }

        public bool AssignPersonalChallenge(Guid userId, ChallengeDto challenge)
        {
            var user = _userRepo.Get(userId);
            if (user == null)
                return false;

            if (user.PersonalChallenges == null)
                user.PersonalChallenges = new Dictionary<Guid, bool>();

            user.PersonalChallenges[challenge.Id] = false;
            return _userRepo.Update(user);
        }
    }
}