using Snus_project.Interfaces.RepositoryInterfaces;
using Snus_project.Models;

namespace Snus_project.Repository;

public class UserRepository : IUserRepository
{
    // private readonly DataContext _context;
    // public UserRepository(DataContext context) { _context = context;}
    
    public ICollection<User> GetUsers()
    {
        // return _context.Users.OrderBy(x => x.Id).ToList();
        return null;
    }

    public User GetByUsernameAndPassword(string username, string password)
    {
        // return _context.Users.Where(x => x.Username==username && x.Password==password).FirstOrDefault();
        return null;

    }
    
    // ovde skinuti komentare kada se ubaci DataContext tj baza pdoataka
}