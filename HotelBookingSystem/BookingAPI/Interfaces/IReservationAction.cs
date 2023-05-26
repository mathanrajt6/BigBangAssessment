using BookingAPI.Models;
using BookingAPI.Models.DTO;

namespace BookingAPI.Interfaces
{
    public interface IReservationAction
    {
        Reservation BookReservation(Reservation reservation);
        Reservation CancelReservation(Reservation reservation);
        Reservation EditReservation(Reservation reservation);
        List<RoomDTO> GetAllBookedRoom(HotelDTO hotelDTO);
        List<Reservation> GetAllReservation(UserDTO userDTO);

    }
}
