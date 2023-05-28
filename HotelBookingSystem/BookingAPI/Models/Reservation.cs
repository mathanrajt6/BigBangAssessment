using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="The Username Should have 50 Characters")]
        public string Username { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="Hotel Id should be positive")]
        [DefaultValue(0)]
        [Required]
        public int HotelId { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="Room Id should be positive")]
        [DefaultValue(0)]
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
    }
}
