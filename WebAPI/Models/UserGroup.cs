namespace WebAPI.Models;

public class UserGroup
{
    public UserGroupCode Code { get; set; }
    public string Description { get; set; }
}

public enum UserGroupCode
{
    Admin,
    User
}
