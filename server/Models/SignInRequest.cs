using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class SignInRequest
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}