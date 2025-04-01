using ServerCore.Models;

namespace ServerCore.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public static UserDto FromUser(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age
            };
        }
    }
}