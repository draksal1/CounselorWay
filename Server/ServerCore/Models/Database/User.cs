using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerCore.Models.Database;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public int Age { get; set; }

    [Required]
    public Characteristics Characteristics { get; set; } = new();

    private readonly Dictionary<Guid, bool> _personalChallenges = new();
    private readonly Dictionary<Guid, bool> _generalChallenges = new();

    public User() { }
}