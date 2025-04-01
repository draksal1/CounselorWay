using ServerCore.Models;

namespace ServerCore.Repositories.Contracts
{
    public interface IUserRepository
    {
        Guid Add(User user);
        User Get(Guid id);
        IEnumerable<User> GetAll();
        bool Update(User user);
        bool Delete(Guid id);
        bool AssignPersonalChallenge(Guid userId, Guid challengeId);
        bool CompleteMapChallenge(Guid userId, Guid mapId, Guid challengeId);
        bool CompletePersonalChallenge(Guid userId, Guid challengeId);
    }
}