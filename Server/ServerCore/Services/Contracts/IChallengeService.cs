using System;
using System.Collections.Generic;
using ServerCore.Models;

namespace ServerCore.Services.Contracts
{
    public interface IChallengeService
    {
        bool AssignPersonalChallenge(Guid userId, Challenge challenge);
        bool CompleteLocationChallenge(Guid userId, Guid mapId, Guid challengeId);
        bool CompletePersonalChallenge(Guid userId, Guid challengeId);
        IEnumerable<Challenge> GetAvailableChallenges(Guid userId, Guid seasonId);
    }
}