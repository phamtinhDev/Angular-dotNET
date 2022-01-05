namespace server.Models;

public class SignInResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }

    public SignInResponse(User user, string token)
    {
        Id = user.Id;
        Username = user.UserName;
        Email = user.Email;
        Token = token;
    }
}