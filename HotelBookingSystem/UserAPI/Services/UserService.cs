using System.Security.Cryptography;
using System.Text;
using UserAPI.Interfaces;
using UserAPI.Models.DTO;

namespace UserAPI.Services
{
    public class UserService : IUserAction
    {
        private readonly IUser _userrepo;
        private readonly ITokenGenerate _tokenService;

        public UserService(IUser userrepo, ITokenGenerate tokenService)
        {
            _userrepo = userrepo;
            _tokenService = tokenService;
        }
        public UserDTO Login(UserDTO userDTO)
        {
            UserDTO user = null;
            var userData = _userrepo.Get(userDTO.Username);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.HashKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.Password[i])
                        return null;
                }
                user = new UserDTO();
                user.Username = userData.Username;
                user.Role = userData.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }

        public UserDTO Register(UserRegisterDTO userDTO)
        {
            var existingUser = _userrepo.Get(userDTO.Username);
            if (existingUser != null)
                return null;
            UserDTO user = null;
            var hmac = new HMACSHA512();
            userDTO.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.PasswordClear));
            userDTO.HashKey = hmac.Key;
            var resultUser = _userrepo.Add(userDTO);
            if (resultUser != null)
            {
                user = new UserDTO();
                user.Username = resultUser.Username;
                user.Role = resultUser.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }

        public UserDTO UpdatePassword(UserUpdateDTO userUpdateDTO)
        {
            var user = _userrepo.Get(userUpdateDTO.username);
            if (user != null)
            {
                var hmac = new HMACSHA512(user.HashKey);
                user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userUpdateDTO.password));
                _userrepo.Update(user);
                var userDTO = new UserDTO();
                userDTO.Username = user.Username;
                userDTO.Role = user.Role;
                userDTO.Token = _tokenService.GenerateToken(userDTO);
                return userDTO;
            }
            return null;
        }
    }
}
