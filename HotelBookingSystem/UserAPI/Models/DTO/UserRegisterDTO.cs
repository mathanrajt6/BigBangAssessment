using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models.DTO
{
    public class UserRegisterDTO : User
    {
        public string PasswordClear { get; set; }
    }
}
