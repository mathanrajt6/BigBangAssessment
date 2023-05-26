using UserAPI.Models.DTO;

namespace UserAPI.Interfaces
{
    public interface iTokenGenerate
    {
        public string GenerateToken(UserDTO user);
    }
}
