using BookingAPI.Models;

namespace BookingAPI.Interfaces
{
    public interface IReservation
    {
        ICollection<Reservation> GetAll();
        Reservation Get(int id);
        Reservation Add(Reservation user);
        Reservation Update(Reservation user);
        Reservation Delete(int id);
    }
}
