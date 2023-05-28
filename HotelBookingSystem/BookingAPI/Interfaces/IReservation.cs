using BookingAPI.Models;

namespace BookingAPI.Interfaces
{
    public interface IReservation 
    {
        ICollection<Reservation> GetAll();
        Reservation Get(int id);
        Reservation Add(Reservation reservation);
        Reservation Update(Reservation reservation);
        Reservation Delete(int id);
    }
}
