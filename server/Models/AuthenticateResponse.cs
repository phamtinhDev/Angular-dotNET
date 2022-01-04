namespace server.Models;

public class AuthenticateResponse
{
    private int Id { get; set; }
    private string Username { get; set; }
    private string Email { get; set; }
    private string Token { get; set; }

    public AuthenticateResponse(User user, string token)
    {
        Id = user.Id;
        Username = user.UserName;
        Email = user.Email;
        Token = token;
    }
}