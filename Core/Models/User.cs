namespace Core.Models;

public class User
{
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserGroup UserGroup { get; set; }
    public UserState UserState { get; set; }
}
