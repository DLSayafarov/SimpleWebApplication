namespace Core.Services;

public interface IUserProviderService
{
    public ServiceRequestResult<Models.User> GetUserByLogin(string login);
    public ServiceRequestResult<List<Models.User>> GetAllUsers();
    public ServiceRequestResult<List<Models.User>> GetAllUsers(int skip, int take);
    public ServiceRequestResult RegisterNewUser(Models.User user);
    public ServiceRequestResult UnregisterUserByLogin(string login);
}