using ServerCore.Models.Database;

namespace ServerCore.Services;

public class ChallangeService
{
    public void AddPersonalChallenge(Guid challengeId)
    {
        //_personalChallenges.Add(new KeyValuePair<Guid, bool>(challengeId, false));
    }

    public void CompletePersonalChallenge(Guid challengeId)
    {
        //var index = _personalChallenges.FindIndex(x => x.Key == challengeId);
        //if (index != -1)
        //{
        //    _personalChallenges[index] = new KeyValuePair<Guid, bool>(challengeId, true);
        //}
    }

    public void AddGeneralChallenge(Guid challengeId)
    {
        //_generalChallenges.Add(new KeyValuePair<Guid, bool>(challengeId, false));
    }

    public void CompleteGeneralChallenge(Guid challengeId)
    {
        //var index = _generalChallenges.FindIndex(x => x.Key == challengeId);
        //if (index != -1)
        //{
        //    _generalChallenges[index] = new KeyValuePair<Guid, bool>(challengeId, true);
        //}
    }

    public IEnumerable<Guid> GetIncompleteChallenges()
    {
        //return _personalChallenges.Where(x => !x.Value).Select(x => x.Key)
        //    .Concat(_generalChallenges.Where(x => !x.Value).Select(x => x.Key));
        throw new NotImplementedException();
    }

    public bool AssignGeneralChallengeToUser(Guid userId, Guid challengeId)
    {
        //if (!_users.TryGetValue(userId, out var user))
        //    return false;

        //user.AddGeneralChallenge(challengeId);
        //return true;
        throw new NotImplementedException();
    }
}
