using UserAPI.Interfaces;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class UserRepo : IUser
    {
        private readonly UserContext _usercontext;
        public UserRepo(UserContext userContext)
        {
            _usercontext = userContext;
        }
        public User Add(User user)
        {
            _usercontext.Users.Add(user);
            _usercontext.SaveChanges();
            return user;
        }

        public User Delete(string username)
        {
            var user = _usercontext.Users.SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                _usercontext.Users.Remove(user);
                _usercontext.SaveChanges();
            }
            return user;
        }

        public User Get(string username)
        {
            return _usercontext.Users.SingleOrDefault(u => u.Username == username);
        }

        public ICollection<User> GetAll()
        {
            return _usercontext.Users.ToList();
        }

        public User Update(User user)
        {
            var userToUpdate = _usercontext.Users.SingleOrDefault(u => u.Username == user.Username);
            if (userToUpdate != null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Age = user.Age;
                userToUpdate.PhoneNumber = user.PhoneNumber;
                userToUpdate.Role = user.Role;
                _usercontext.SaveChanges();
            }
            return userToUpdate;
        }
    }
}
