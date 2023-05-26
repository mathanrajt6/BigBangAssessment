using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int HotelId { get; set; }
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
