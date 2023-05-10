using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;

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
        });
        _mapper = new Mapper(config);
    }
    
    public User GetUserByName(string username)
    {
        return _mapper.Map<DataAccess.Entities.User, User>(_dbContext.Users.FirstOrDefault());
    }

    public List<User> GetAllUsers()
    {
        return _dbContext.Users.Select(_mapper.Map<DataAccess.Entities.User, User>).ToList();
    }

    public List<User> GetAllUsers(int skip, int take)
    {
        return _dbContext.Users.Skip(skip).Take(take).AsEnumerable()
            .Select(_mapper.Map<DataAccess.Entities.User, User>).ToList();
    }

    public void AddNewUser(User user)
    {
        _dbContext.Users.Add(_mapper.Map<User, DataAccess.Entities.User>(user));
        _dbContext.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        _dbContext.Users.Remove(_mapper.Map<User, DataAccess.Entities.User>(user));
        _dbContext.SaveChanges();
    }
}

/*public class Mapper
{
    public User Map(DataAccess.Entities.User user)
    {
        if (user is null)
            return null;
        return new User
        {
            Login = user.Login,
            Password = user.Password,
            CreatedDate = user.CreatedDate,
            UserGroup = Map(user.UserGroup),
            UserState = Map(user.UserState)
        };
    }
    
    public UserGroup Map(DataAccess.Entities.UserGroup userGroup)
    {
        return new UserGroup
        {
            Code = Map(userGroup.Code),
            Description = userGroup.Description
        };
    }
    
    public UserState Map(DataAccess.Entities.UserState userState)
    {
        return new UserState
        {
            Code = Map(userState.Code),
            Description = userState.Description
        };
    }

    public UserGroupCode Map(DataAccess.Entities.UserGroupCode userGroupCode) => (UserGroupCode)userGroupCode;
    public UserStateCode Map(DataAccess.Entities.UserStateCode userStateCode) => (UserStateCode)userStateCode;
    
    public DataAccess.Entities.User Map(User user)
    {
        if (user is null)
            return null;
        return new DataAccess.Entities.User
        {
            Login = user.Login,
            Password = user.Password,
            CreatedDate = user.CreatedDate,
            UserGroup = Map(user.UserGroup),
            UserState = Map(user.UserState)
        };
    }
    
    public DataAccess.Entities.UserGroup Map(UserGroup userGroup)
    {
        return new DataAccess.Entities.UserGroup
        {
            Code = Map(userGroup.Code),
            Description = userGroup.Description
        };
    }
    
    public DataAccess.Entities.UserState Map(UserState userState)
    {
        return new DataAccess.Entities.UserState
        {
            Code = Map(userState.Code),
            Description = userState.Description
        };
    }

    public UserGroupCode Map(DataAccess.Entities.UserGroupCode userGroupCode) => (UserGroupCode)userGroupCode;
    public UserStateCode Map(DataAccess.Entities.UserStateCode userStateCode) => (UserStateCode)userStateCode;
}*/