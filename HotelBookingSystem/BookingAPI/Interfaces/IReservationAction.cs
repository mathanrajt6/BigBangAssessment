using BookingAPI.Models;
using BookingAPI.Models.DTO;

namespace BookingAPI.Interfaces
{
    public interface IReservationAction
    {
        Reservation BookReservation(Reservation reservation);
        Reservation CancelReservation(int id);
        Reservation EditReservation(Reservation reservation);
        List<RoomDTO> GetAllBookedRoom(int hotelid);
        List<Reservation> GetAllReservation();

    }
}
