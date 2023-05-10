namespace Core;

public interface IUserRepository
{
    public Models.User GetUserByName(string username);
    public List<Models.User> GetAllUsers();
    public List<Models.User> GetAllUsers(int skip, int take);
    public void AddNewUser(Models.User user);
    public void DeleteUser(Models.User username);
}