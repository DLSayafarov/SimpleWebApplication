using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly Core.Services.IUserProviderService _userProviderService;
    private readonly Mapper _mapper;

    public UsersController(ILogger<UsersController> logger, Core.Services.IUserProviderService userProviderService)
    {
        _logger = logger;
        _userProviderService = userProviderService;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, Core.Models.User>();
            cfg.CreateMap<Core.Models.User, User>();
            cfg.CreateMap<UserGroup, Core.Models.UserGroup>();
            cfg.CreateMap<Core.Models.UserGroup, UserGroup>();
            cfg.CreateMap<UserState, Core.Models.UserState>();
            cfg.CreateMap<Core.Models.UserState, UserState>();
        });
        _mapper = new Mapper(config);
    }

    [HttpGet]
    [Route("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var serviceResponse = _userProviderService.GetAllUsers();
        if (serviceResponse.IsSuccessful)
        {
            _logger.Log(LogLevel.Information, "Successful GetAllUsers request processing");
            return Ok(serviceResponse.Result.Select(u => _mapper.Map<Core.Models.User, User>(u)));    
        }
        
        _logger.Log(LogLevel.Warning, "GetAllUsers request processing failed");
        return BadRequest(serviceResponse.Description);
    }
    
    [HttpGet]
    [Route("GetUser")]
    public async Task<IActionResult> GetUser(string userLogin)
    {
        var serviceResponse = _userProviderService.GetUserByLogin(userLogin);
        if (serviceResponse.IsSuccessful)
        {
            _logger.Log(LogLevel.Information, "Successful GetUser request processing");
            return Ok(_mapper.Map<Core.Models.User, User>(serviceResponse.Result));
        }
        
        _logger.Log(LogLevel.Information, $"User {userLogin} not found");
        return BadRequest(serviceResponse.Description);
    }
    
    [HttpPost]
    [Route("RegisterNewUser")]
    public async Task<IActionResult> RegisterNewUser(User user)
    {
        var serviceResponse = _userProviderService.RegisterNewUser(_mapper.Map<User, Core.Models.User>(user));
        if (serviceResponse.IsSuccessful)
        {
            _logger.Log(LogLevel.Information, $"User {user.Login} registered");
            return Ok();
        }
        _logger.Log(LogLevel.Information, $"User {user.Login} failed to register");
        return BadRequest(serviceResponse.Description);
    }
    
    [HttpPost]
    [Route("DeleteUser")]
    public async Task<IActionResult> DeleteUser(string userLogin)
    {
        var serviceResponse = _userProviderService.UnregisterUserByLogin(userLogin);
        if (serviceResponse.IsSuccessful)
        {
            _logger.Log(LogLevel.Information, $"User {userLogin} unregistered");
            return Ok();
        }
        _logger.Log(LogLevel.Information, $"User {userLogin} failed to unregister");
        return BadRequest(serviceResponse.Description);
    }
}