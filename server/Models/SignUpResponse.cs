namespace server.Models;

public class SignUpResponse
{
    public int Id { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }

    public SignUpResponse(User user)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
    }
}