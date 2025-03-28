using System.ComponentModel.DataAnnotations;
using ServerCore.Models.Database;

namespace ServerCore.Models.Api.Users;

public class UserUpdate
{
    [Required]
    public Guid Id { get; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int Age { get; set; }

    [Required]
    public Characteristics Characteristics { get; } = new();

    public void CopyTo(User value)
    {
        value.Id = Id;
        value.Name = Name;
        value.Age = Age;
        value.Characteristics = Characteristics;
    }
}
