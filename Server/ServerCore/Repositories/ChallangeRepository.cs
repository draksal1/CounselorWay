using System.Collections.Generic;
using ServerCore.Models.Database;

namespace ServerCore.Repositories
{
    public class ChallengeRepository
    {
        private readonly Dictionary<Guid, Challenge> _challenges = new();
        private readonly List<Challenge> _generalChallenges = new();

        public Guid AddPersonalChallenge(Challenge challenge)
        {
            _challenges[challenge.Id] = challenge;
            return challenge.Id;
        }

        public Guid AddGeneralChallenge(Challenge challenge)
        {
            _generalChallenges.Add(challenge);
            return challenge.Id;
        }

        public Challenge GetChallenge(Guid id)
        {
            return _challenges.TryGetValue(id, out var challenge)
                ? challenge
                : _generalChallenges.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Challenge> GetAllGeneralChallenges()
        {
            return _generalChallenges.AsReadOnly();
        }

        public bool RemoveChallenge(Guid id)
        {
            return _challenges.Remove(id) ||
                   _generalChallenges.RemoveAll(c => c.Id == id) > 0;
        }
    }
}