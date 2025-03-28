using ServerCore.Models.Database;

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
    }
}