using BookingAPI.Models;
using BookingAPI.Models.DTO;

namespace BookingAPI.Interfaces
{
    public interface IReservationAction : ICount
    {
        Reservation BookReservation(Reservation reservation);
        Reservation CancelReservation(ReservationDTO reservationDTO);
        Reservation EditReservation(Reservation reservation);
        List<RoomDTO> GetAllBookedRoom(HotelDTO hotelDTO);
        List<Reservation> GetAllReservation(UserDTO userDTO);



    }
}
