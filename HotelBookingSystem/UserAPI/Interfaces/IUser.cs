using UserAPI.Models;

namespace UserAPI.Interfaces
{
    public interface IUser
    {
        ICollection<User> GetAll();
        User Get(string username);
        User Add(User user);
        User Update(User user);
        User Delete(string username);
    }
}
