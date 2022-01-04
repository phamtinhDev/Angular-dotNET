using Microsoft.AspNetCore.Mvc;
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
    
    [HttpGet("/users")]
    public async Task<List<User>> GetUsers()
    {
        var users = await _userService.GetUsers();

        return users;
    }
}