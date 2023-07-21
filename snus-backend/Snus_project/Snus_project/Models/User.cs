using System.ComponentModel.DataAnnotations;
using Snus_project.Models.enums;

namespace Snus_project.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public String Username { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }
    public Role Role { get; set; }


    public User(String username, String email, String password, Role role)
    {
        Username     = username;
        Email        = email;
        Password     = password;
        Role         = role;
    }


}