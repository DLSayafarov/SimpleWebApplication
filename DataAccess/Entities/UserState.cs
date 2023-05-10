using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class UserState
{
    [Key]
    public int Id { get; set; }
    [Required]
    public UserStateCode Code { get; set; }
    [Required]
    public string Description { get; set; }
}

public enum UserStateCode
{
    Active,
    Blocked
}
