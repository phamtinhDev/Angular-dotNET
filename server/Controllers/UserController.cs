using Microsoft.AspNetCore.Mvc;
using server.Helpers;
using server.Models;
using server.Services.User;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Authorize]
    [HttpGet("/users")]
    public List<User> GetUsers()
    {
        var users = _userService.GetUsers();

        return users;
    }
}