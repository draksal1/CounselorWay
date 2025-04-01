using System;
using System.Collections.Generic;
using ServerCore.Models;

namespace ServerCore.Services.Contracts
{
    public interface IAdminService
    {
        Guid CreateSeason(string name);
        Guid AddChallengeMap(Guid seasonId, string mapName);
        void AddChallengeToMap(Guid mapId, Challenge challenge, bool assignToAll);
        Guid CreateUser(string name);
        void AssignChallengeToUser(Guid userId, Guid challengeId);
    }
}