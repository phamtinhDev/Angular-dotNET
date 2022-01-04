using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.User;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/sign-up")]
    public async Task<ActionResult<int>> SignUp(User user)
    {
        return await _userService.CreateUser(user);
    }

    [HttpPost("/sign-in")]
    public ActionResult<User> SignIn(AuthenticateRequest dataLogin)
    {
        var user = _userService.FindUser(dataLogin);
        
        if(user == null) return NotFound();
    
        return user;
    }
}