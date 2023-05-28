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

        /// <summary>
        /// This method is used to inject the dependencies
        /// </summary>
        /// <param name="userrepo"></param>
        /// <param name="tokenService"></param>
        public UserService(IUser userrepo, ITokenGenerate tokenService)
        {
            _userrepo = userrepo;
            _tokenService = tokenService;
        }

        /// <summary>
        /// This method is used to Login a  user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>UserDTO </returns>
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


        /// <summary>
        /// This method is used to register a new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>UserDTO</returns>
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

        /// <summary>
        /// This method is used to update password of a user
        /// </summary>
        /// <param name="userUpdateDTO"></param>
        /// <returns>UserUpdateDTO</returns>
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
