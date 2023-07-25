using SNUS_PROJECT.Data;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public User AddUser(User user)
        {
            var entityEntry = _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
            return entityEntry.Entity;
        }

        public User GetUser(int id)
        {
            return _dataContext.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User? GetUser(string username)
        {
            return _dataContext.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
        }

        public User Login(string username, string password)
        {
            return _dataContext.Users.Where(u => u.Username.Equals(username) && u.Password.Equals(password)).FirstOrDefault();
        }
    }
}
