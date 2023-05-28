using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class Room : IEquatable<Room>
    {
        [Key]
        public int RoomId { get; set; }
        
        [Range (1, int.MaxValue,ErrorMessage ="HotelId shoule be positive")]
        [DefaultValue(0)]
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Room number shoule be positive")]
        [DefaultValue(0)]
        public int RoomNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Capacity number shoule be positive")]
        [DefaultValue(0)]
        [Required]
        public int Capacity { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Price  shoule be positive")]
        [DefaultValue(0)]
        [Required]
        public double Price { get; set; }

        [Required]
        public bool AC { get; set; }

        public bool Equals(Room? other)
        {
            return this.HotelId == other.HotelId && this.RoomId == other.RoomId;
        }
    }
}
