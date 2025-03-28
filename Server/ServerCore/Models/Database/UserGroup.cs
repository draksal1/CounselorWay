using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerCore.Models.Database;

public class UserGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public Guid Id { get; set; }

    [Key]
    [Required]
    public string Key { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
}
