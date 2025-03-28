using System.ComponentModel.DataAnnotations;
using ServerCore.Models.Database;

namespace ServerCore.Models.Api.Users;

public class UserCreate
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int Age { get; set; }

    public Characteristics Characteristics { get; set; } = new();

    public void CopyTo(User value)
    {
        value.Name = Name;
        value.Age = Age;
        value.Characteristics = Characteristics;
    }
}
