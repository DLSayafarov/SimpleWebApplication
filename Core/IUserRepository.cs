namespace Core;

public interface IUserRepository
{
    public Models.User GetUserByLogin(string login);
    public List<Models.User> GetAllUsers();
    public List<Models.User> GetAllUsers(int skip, int take);
    public void CreateOrUpdateUser(Models.User user);
    public void UnregisterUserByLogin(string login);
    public bool DoesUserExists(string login);
    public bool DoesAdminExists();
}