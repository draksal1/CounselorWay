using System;
using ServerCore.DTOs;
using ServerCore.Models;

namespace ServerCore.Services.Contracts
{
    public interface IUserService
    {
        Guid RegisterUser(string name, int age);
        User GetUser(Guid id);
        bool UpdateUser(Guid userId, Action<User> updateAction);
        bool UpdateCharacteristics(Guid userId, Action<Characteristics> updateAction);

        bool AssignPersonalChallenge(Guid userId, ChallengeDto challenge);
    }
}