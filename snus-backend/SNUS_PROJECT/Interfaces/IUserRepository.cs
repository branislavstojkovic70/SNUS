using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Interfaces
{
    public interface IUserRepository
    {
        public User GetUser(int id);
        public User? GetUser(string email);
        public User AddUser(User user);
        public User Login(string username, string password);
    }
}
