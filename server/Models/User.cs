using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class User
{
    [Key] public int UserId { get; set; }

    [Column(TypeName = "nvarchar(50)")] public string UserName { get; set; }

    [Column(TypeName = "nvarchar(50)")] public string Email { get; set; }

    [Column(TypeName = "nvarchar(30)")] public string Password { get; set; }
}