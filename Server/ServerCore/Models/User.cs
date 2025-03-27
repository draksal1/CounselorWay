using System.Collections.Generic;

namespace ServerCore.Models
{
    public class User
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Age { get; set; }
        public Characteristics Characteristics { get; } = new Characteristics();

        private List<KeyValuePair<Guid, bool>> _personalChallenges = new();
        private List<KeyValuePair<Guid, bool>> _generalChallenges = new();

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void AddPersonalChallenge(Guid challengeId)
        {
            _personalChallenges.Add(new KeyValuePair<Guid, bool>(challengeId, false));
        }

        public void CompletePersonalChallenge(Guid challengeId)
        {
            var index = _personalChallenges.FindIndex(x => x.Key == challengeId);
            if (index != -1)
            {
                _personalChallenges[index] = new KeyValuePair<Guid, bool>(challengeId, true);
            }
        }

        public void AddGeneralChallenge(Guid challengeId)
        {
            _generalChallenges.Add(new KeyValuePair<Guid, bool>(challengeId, false));
        }

        public void CompleteGeneralChallenge(Guid challengeId)
        {
            var index = _generalChallenges.FindIndex(x => x.Key == challengeId);
            if (index != -1)
            {
                _generalChallenges[index] = new KeyValuePair<Guid, bool>(challengeId, true);
            }
        }

        public IEnumerable<Guid> GetIncompleteChallenges()
        {
            return _personalChallenges.Where(x => !x.Value).Select(x => x.Key)
                .Concat(_generalChallenges.Where(x => !x.Value).Select(x => x.Key));
        }
    }
}