using BookingAPI.Interfaces;
using BookingAPI.Models;
using System.Diagnostics;

namespace BookingAPI.Services
{
    public class ReservationRepo : IReservation
    {
        private ReservationContext _context;

        /// <summary>
        /// This method is used to inject the dependencies
        /// </summary>
        /// <param name="context"></param>
        public ReservationRepo(ReservationContext context ) {
            _context = context;
        }

        /// <summary>
        /// This method adds a reservation to the database.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns>Reservation</returns>
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

        /// <summary>
        /// This method deletes a reservation from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Reservation</returns>
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

        /// <summary>
        /// This method gets a reservation from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Reservation</returns>
        public Reservation Get(int id)
        {
            return _context.Reservations.SingleOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// This method gets all reservations from the database.
        /// </summary>
        /// <returns>List of Reservation</returns>
        public ICollection<Reservation> GetAll()
        {
            return _context.Reservations.ToList();
        }

        /// <summary>
        /// This method updates a reservation in the database.
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns>Reservation</returns>
        public Reservation Update(Reservation reservation)
        {
            var existingreservation = _context.Reservations.SingleOrDefault( r=>r.Id == reservation.Id );
            if(existingreservation != null )
            {
                existingreservation.CheckIn = reservation.CheckIn;
                existingreservation.CheckOut = reservation.CheckOut;
                existingreservation.RoomId = reservation.RoomId;
                _context.SaveChanges();
            }
            return existingreservation;
        }
    }
}
