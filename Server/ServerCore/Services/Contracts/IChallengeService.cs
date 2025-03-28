using System;
using System.Collections.Generic;
using ServerCore.Models.Database;

namespace ServerCore.Services.Contracts
{
    public interface IChallengeService
    {
        Guid CreatePersonalChallenge(string name, string description, int cost);
        Guid CreateGeneralChallenge(string name, string description, int cost);
        bool CompleteChallenge(Guid userId, Guid challengeId);
        IEnumerable<Challenge> GetAvailableChallenges(Guid userId);
    }
}