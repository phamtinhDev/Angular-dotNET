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
    public async Task<ActionResult<int>> SignUp(SignUpRequest user)
    {
        return await _userService.CreateUser(user);
    }

    [HttpPost("/sign-in")]
    public ActionResult SignIn(SignInRequest dataLogin)
    {
        var response = _userService.Authenticate(dataLogin);

        if (response == null)
        {
            return BadRequest(new {message = "Username or password is incorrect"});
        }
        
        return Ok(response);
    }
}