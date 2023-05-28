using UserAPI.Interfaces;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class UserRepo : IUser
    {
        private readonly UserContext _usercontext;

        /// <summary>
        /// This method is used to inject the dependencies
        /// </summary>
        /// <param name="userContext"></param>
        public UserRepo(UserContext userContext)
        {
            _usercontext = userContext;
        }

        /// <summary>
        /// This method adds a user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User</returns>
        public User Add(User user)
        {
            _usercontext.Users.Add(user);
            _usercontext.SaveChanges();
            return user;
        }

        /// <summary>
        /// This method deletes a user from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User</returns>
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

        /// <summary>
        /// This method gets a user from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User</returns>
        public User Get(string username)
        {
            return _usercontext.Users.SingleOrDefault(u => u.Username == username);
        }

        /// <summary>
        /// This method gets all the users from the database
        /// </summary>
        /// <returns>List of User</returns>
        public ICollection<User> GetAll()
        {
            return _usercontext.Users.ToList();
        }

        /// <summary>
        /// This method updates a user in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User</returns>
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
