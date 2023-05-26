using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Hotel : IEquatable<Hotel>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }


        public bool Equals(Hotel? other)
        {
            if (other == null)
            {
                return false;
            }
            if (this.Name == other.Name && this.Address == other.Address && this.City == other.City && this.Country == other.Country && this.Phone == other.Phone && this.Email == other.Email)
            {
                return true;
            }
            return false;
        }

    }

   

}
