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
    public IActionResult GetAllUsers()
    {
        var serviceResponse = _userProviderService.GetAllUsers();
        if (serviceResponse.IsSuccessful)
            return Ok(serviceResponse.Result.Select(u => _mapper.Map<Core.Models.User, User>(u)));

        return BadRequest(serviceResponse.Description);
    }
    
    [HttpGet]
    [Route("GetUser")]
    public IActionResult GetUser(string userLogin)
    {
        var serviceResponse = _userProviderService.GetUserByLogin(userLogin);
        if (serviceResponse.IsSuccessful)
            return Ok(_mapper.Map<Core.Models.User, User>(serviceResponse.Result));

        return BadRequest(serviceResponse.Description);
    }
    
    [HttpPost]
    [Route("RegisterNewUser")]
    public IActionResult RegisterNewUser(User user)
    {
        var serviceResponse = _userProviderService.RegisterNewUser(_mapper.Map<User, Core.Models.User>(user));
        return serviceResponse.IsSuccessful ? Ok() : BadRequest(serviceResponse.Description);
    }
    
    [HttpPost]
    [Route("DeleteUser")]
    public IActionResult DeleteUser(string userLogin)
    {
        var serviceResponse = _userProviderService.UnregisterUserByLogin(userLogin);
        return serviceResponse.IsSuccessful ? Ok() : BadRequest(serviceResponse.Description);
    }
}