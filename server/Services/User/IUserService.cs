

using server.Models;

namespace server.Services.User;

public interface IUserService
{
    Task<List<Models.User>> GetUsers();
    
    Task<int> CreateUser(Models.User user);

    Models.User? FindUser(AuthenticateRequest dataLogin);

    public AuthenticateResponse? Authenticate(AuthenticateRequest data);
}