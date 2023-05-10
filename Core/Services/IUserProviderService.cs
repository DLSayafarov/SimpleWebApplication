namespace Core.Services;

public interface IUserProviderService
{
    public Models.User GetUserByName(string username);
    public List<Models.User> GetAllUsers();
    public List<Models.User> GetAllUsers(int skip, int take);
    public ServiceRequestResult RegisterNewUser(Models.User user);
    public ServiceRequestResult DeleteUserByName(string username);
}