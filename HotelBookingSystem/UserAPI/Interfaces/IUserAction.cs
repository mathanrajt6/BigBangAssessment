using UserAPI.Models.DTO;

namespace UserAPI.Interfaces
{
    public interface IUserAction
    {
        UserDTO Login(UserDTO userDTO);
        UserDTO Register(UserRegisterDTO userDTO);
        UserDTO UpdatePassword(UserUpdateDTO userDTO);
    }
}
