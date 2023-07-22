using Snus_project.Models;

namespace Snus_project.Interfaces.RepositoryInterfaces;

public interface IUserRepository
{
    ICollection<User> GetUsers();

    User GetByUsernameAndPassword(string username, string password);
}