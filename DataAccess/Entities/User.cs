using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public UserGroup UserGroup { get; set; }
    [Required]
    public UserState UserState { get; set; }
}

