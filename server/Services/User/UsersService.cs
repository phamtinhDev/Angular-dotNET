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

    public async Task<List<Models.User>> GetUsers()
    {
        return await _dbContext.User.ToListAsync();
    }

    public async Task<int> CreateUser(Models.User user)
    {
        _dbContext.User.Add(user);

        return await _dbContext.SaveChangesAsync();
    }

    public Models.User? FindUser(AuthenticateRequest dataLogin)
    {
        var user = _dbContext.User.FirstOrDefault(user =>
            user.UserName == dataLogin.Username && user.Password == dataLogin.Password);

        if (user == null) return null;

        return user;
    }

    public AuthenticateResponse? Authenticate(AuthenticateRequest data)
    {
        var user = FindUser(data);
        
        if(user == null) return null;

        var token = GenerateJwt(user);
        
        return new AuthenticateResponse(user, token);
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