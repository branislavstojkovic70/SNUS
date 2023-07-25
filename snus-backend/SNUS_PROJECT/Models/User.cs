using SNUS_PROJECT.DTO;
using System.Data;

namespace SNUS_PROJECT.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }

        public User()
        {

        }

        public User(string username, string password, int role)
        {
            this.Username = username;
            this.Password = password;
            this.Role = role;
        }

        public User(UserDto userDto)
        {
            this.Username = userDto.Username;
            this.Password = userDto.Password;
            this.Role = 1;
        }
    }
}
