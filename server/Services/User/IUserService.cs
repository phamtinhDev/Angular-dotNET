

using server.Models;

namespace server.Services.User;

public interface IUserService
{
    List<Models.User> GetUsers();
    
    Task<int> CreateUser(Models.SignUpRequest user);

    Models.User? FindUser(SignInRequest dataLogin);

    public Models.User? FindUserById(int Id);

    public SignInResponse? Authenticate(SignInRequest data);
}