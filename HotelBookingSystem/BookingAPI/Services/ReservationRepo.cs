using BookingAPI.Interfaces;
using BookingAPI.Models;
using System.Diagnostics;

namespace BookingAPI.Services
{
    public class ReservationRepo : IReservation
    {
        private ReservationContext _context;
        public ReservationRepo(ReservationContext context ) {
            _context = context;
        }
        public Reservation Add(Reservation reservation)
        {
            try
            {
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
                return reservation;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;    
            }
        }

        public Reservation Delete(int id)
        {
            var reservation = _context.Reservations.SingleOrDefault( r=>r.Id == id );
            if( reservation != null )
            {
                _context.Reservations.Remove( reservation );
                _context.SaveChanges();
            }
            return reservation;
        }

        public Reservation Get(int id)
        {
            return _context.Reservations.SingleOrDefault(r => r.Id == id);
        }

        public ICollection<Reservation> GetAll()
        {
            return _context.Reservations.ToList();
        }

        public Reservation Update(Reservation reservation)
        {
            var existingreservation = _context.Reservations.SingleOrDefault( r=>r.Id == reservation.Id );
            if(existingreservation != null )
            {
                existingreservation.CheckIn = reservation.CheckIn;
                existingreservation.CheckOut = reservation.CheckOut;
                existingreservation.RoomId = reservation.RoomId;
                existingreservation.HotelId = reservation.HotelId;
                existingreservation.Username = reservation.Username;
                _context.SaveChanges();
            }
            return existingreservation;
        }
    }
}
