using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using UserGroupCode = DataAccess.Entities.UserGroupCode;
using UserStateCode = DataAccess.Entities.UserStateCode;

namespace DataAccess;

public class UserRepository : Core.IUserRepository
{
    private readonly UsersContext _dbContext;
    private readonly Mapper _mapper;
    
    public UserRepository(UsersContext dbContext)
    {
        _dbContext = dbContext;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, DataAccess.Entities.User>();
            cfg.CreateMap<DataAccess.Entities.User, User>();
            cfg.CreateMap<UserGroup, DataAccess.Entities.UserGroup>();
            cfg.CreateMap<DataAccess.Entities.UserGroup, UserGroup>();
            cfg.CreateMap<UserState, DataAccess.Entities.UserState>();
            cfg.CreateMap<DataAccess.Entities.UserState, UserState>();
        });
        _mapper = new Mapper(config);
    }
    
    public User GetUserByLogin(string login)
    {
        return _mapper.Map<DataAccess.Entities.User, User>(_dbContext.Users.FirstOrDefault(u => u.Login == login));
    }

    public List<User> GetAllUsers()
    {
        return _dbContext.Users.Include(c => c.UserGroup).Include(c => c.UserState)
            .Select(_mapper.Map<DataAccess.Entities.User, User>).ToList();
    }

    public List<User> GetAllUsers(int skip, int take)
    {
        return _dbContext.Users.Include(c => c.UserGroup).Include(c => c.UserState).Skip(skip).Take(take).AsEnumerable()
            .Select(_mapper.Map<DataAccess.Entities.User, User>).ToList();
    }

    public void CreateOrUpdateUser(User user)
    {
        var userEntity = _dbContext.Users.FirstOrDefault(u => u.Login == user.Login);
        if (userEntity is not null)
            _dbContext.Users.Remove(userEntity);
        _dbContext.Users.Add(_mapper.Map<User, DataAccess.Entities.User>(user));
        _dbContext.SaveChanges();
    }

    public void UnregisterUserByLogin(string login)
    {
        var user = _dbContext.Users.First(u => u.Login == login);
        user.UserState.Code = UserStateCode.Blocked;
        _dbContext.SaveChanges();
    }

    public bool DoesUserExists(string login)
    {
        return _dbContext.Users.Any(u => u.Login == login && u.UserState.Code == UserStateCode.Active);
    }
    
    public bool DoesAdminExists()
    {
        return _dbContext.Users.Any(u =>
            u.UserGroup.Code == UserGroupCode.Admin && u.UserState.Code == UserStateCode.Active);
    }
}