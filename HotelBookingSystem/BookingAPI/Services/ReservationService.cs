using BookingAPI.Interfaces;
using BookingAPI.Models;
using BookingAPI.Models.DTO;

namespace BookingAPI.Services
{
    public class ReservationService : IReservationAction
    {
        private readonly IReservation _resrepo;
        public ReservationService(IReservation resrepo)
        {
            _resrepo = resrepo;
        }
        public Reservation BookReservation(Reservation reservation)
        {
            var reservations = _resrepo.GetAll();
            foreach (var item in reservations)
            {
                if (item.RoomId == reservation.RoomId && item.HotelId==reservation.HotelId)
                {
                    if (reservation.CheckIn >= item.CheckIn && reservation.CheckIn <= item.CheckOut)
                    {
                        return null;
                    }
                    else if (reservation.CheckOut >= item.CheckIn && reservation.CheckOut <= item.CheckOut)
                    {
                        return null;
                    }
                    else if (reservation.CheckIn <= item.CheckIn && reservation.CheckOut >= item.CheckOut)
                    {
                        return null;
                    }
                }
            }
            reservation.CheckOut = reservation.CheckOut.Date;
            reservation.CheckIn = reservation.CheckIn.Date;
            return _resrepo.Add(reservation);

        }

        public Reservation CancelReservation(Reservation reservation)
        {
            return _resrepo.Delete(reservation.Id);
        }

        public Reservation EditReservation(Reservation reservation)
        {
            reservation.CheckOut = reservation.CheckOut.Date;
            reservation.CheckIn = reservation.CheckIn.Date;
            return _resrepo.Update(reservation);
        }

        public List<RoomDTO> GetAllBookedRoom(HotelDTO hotelDTO)
        {
            var reservations = _resrepo.GetAll();
            List<RoomDTO> rooms = new List<RoomDTO>();
            foreach (var item in reservations)
            {
                if (item.HotelId == hotelDTO.Id)
                {
                    RoomDTO room = new RoomDTO();
                    room.Id = item.RoomId;
                    rooms.Add(room);

                }
            }
            return rooms;
        }

        public List<Reservation> GetAllReservation(UserDTO userDTO)
        {
            return _resrepo.GetAll().Where(r=>r.Username == userDTO.Username).ToList();
        }
    }
}
