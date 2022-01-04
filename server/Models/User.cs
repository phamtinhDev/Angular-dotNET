using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace server.Models;

public class User
{
    [Key] 
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(50)")] 
    public string UserName { get; set; }

    [Column(TypeName = "nvarchar(50)")] 
    public string Email { get; set; }
    
    [JsonIgnore]
    [Column(TypeName = "nvarchar(30)")] 
    public string Password { get; set; }
}