using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using server.Helpers;
using server.Providers;
using server.Models;

namespace server.Services.User;

public class UserService : IUserService
{
    private readonly DataContext _dbContext;
    private readonly AppSetting _appSetting;

    public UserService(DataContext dbContext, IOptions<AppSetting> appSetting)
    {
        _dbContext = dbContext;
        _appSetting = appSetting.Value;
    }

    public List<Models.User> GetUsers()
    {
        return _dbContext.User.ToList();
    }

    public async Task<int> CreateUser(Models.SignUpRequest user)
    {
        var userData = new Models.User
        {
            Email = user.Email,
            Password = user.Password,
            UserName = user.UserName
        };

        _dbContext.User.Add(userData);

        return await _dbContext.SaveChangesAsync();
    }

    public Models.User? FindUser(SignInRequest dataLogin)
    {
        var user = _dbContext.User.FirstOrDefault(user =>
            user.UserName == dataLogin.Username && user.Password == dataLogin.Password);

        if (user == null) return null;

        return user;
    }
    
    public Models.User? FindUserById(int id)
    {
        var user = _dbContext.User.FirstOrDefault(user => user.Id == id);

        if (user == null) return null;

        return user;
    }

    public SignInResponse? Authenticate(SignInRequest data)
    {
        var user = FindUser(data);
        
        if(user == null) return null;

        var token = GenerateJwt(user);
        
        return new SignInResponse(user, token);
    }

    private string GenerateJwt(Models.User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }
}