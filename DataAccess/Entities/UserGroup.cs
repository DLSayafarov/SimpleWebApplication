using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class UserGroup
{
    [Key]
    public int Id { get; set; }
    [Required]
    public UserGroupCode Code { get; set; }
    [Required]
    public string Description { get; set; }
}

public enum UserGroupCode
{
    Admin,
    User
}
