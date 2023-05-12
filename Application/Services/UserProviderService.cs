using System.Collections.Concurrent;
using Core;
using Core.Models;
using Core.Services;

namespace Application.Services;

public class UserProviderService : IUserProviderService
{
    private readonly IUserRepository _userRepository;
    private readonly ConcurrentDictionary<string, bool> _userLoginsInRegistrationProcess;

    public UserProviderService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _userLoginsInRegistrationProcess = new ConcurrentDictionary<string, bool>();
    }
    public ServiceRequestResult<User> GetUserByLogin(string login)
    {
        var user = _userRepository.GetUserByLogin(login);
        return user is null
            ? new ServiceRequestResult<User>(false, "User not found")
            : new ServiceRequestResult<User>(user);
    }

    public ServiceRequestResult<List<User>> GetAllUsers()
    {
        return new ServiceRequestResult<List<User>>(_userRepository.GetAllUsers());
    }

    public ServiceRequestResult<List<User>> GetAllUsers(int skip, int take)
    {
        return new ServiceRequestResult<List<User>>(_userRepository.GetAllUsers(skip, take));
    }

    public ServiceRequestResult RegisterNewUser(User user)
    {
        if (_userLoginsInRegistrationProcess.ContainsKey(user.Login))
            return new ServiceRequestResult(false, "User already in registration process");

        if (_userRepository.DoesUserExists(user.Login))
            return new ServiceRequestResult(false, "User already exists");
        
        if (user.UserGroup.Code == UserGroupCode.Admin && _userRepository.DoesAdminExists())
            return new ServiceRequestResult(false, "Admin already exists");

        Task.Run(() => StartUserRegistration(user))
            .ContinueWith(_ => FinishRegistration(user));

        return new ServiceRequestResult(true);
    }

    public ServiceRequestResult UnregisterUserByLogin(string login)
    {
        if (_userLoginsInRegistrationProcess.ContainsKey(login))
            return new ServiceRequestResult(false, "User is in registration process");

        if (!_userRepository.DoesUserExists(login))
            return new ServiceRequestResult(false, "User doesn't exists");
        
        _userRepository.UnregisterUserByLogin(login);
        return new ServiceRequestResult(true);
    }

    async private Task StartUserRegistration(User user)
    {
        _userLoginsInRegistrationProcess[user.Login] = true;
        /* do something useful or not so */
        Thread.Sleep(5000);
    }

    private void FinishRegistration(User user)
    {
        _userLoginsInRegistrationProcess.TryRemove(user.Login, out _);
        user.UserState.Code = UserStateCode.Active;
        _userRepository.CreateOrUpdateUser(user);
    }
}