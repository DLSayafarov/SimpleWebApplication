namespace WebAPI.Models;

public class UserState
{
    public UserStateCode Code { get; set; }
    public string Description { get; set; }
}

public enum UserStateCode
{
    Active,
    Blocked
}
