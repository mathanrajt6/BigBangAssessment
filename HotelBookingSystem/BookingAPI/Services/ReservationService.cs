using BookingAPI.Interfaces;
using BookingAPI.Models;
using BookingAPI.Models.DTO;

namespace BookingAPI.Services
{
    public class ReservationService : IReservationAction
    {
        private readonly IReservation _resrepo;

        /// <summary>
        /// This method is used to inject the dependencies
        /// </summary>
        /// <param name="resrepo"></param>
        public ReservationService(IReservation resrepo)
        {
            _resrepo = resrepo;
        }

        /// <summary>
        /// This method is used to book a reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns>Reservation</returns>
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

        /// <summary>
        /// This method is used to cancel a reservation
        /// </summary>
        /// <param name="reservationDTO"></param>
        /// <returns>Reservation</returns>
        public Reservation CancelReservation(ReservationDTO reservationDTO)
        {
            return _resrepo.Delete(reservationDTO.Id);
        }

        /// <summary>
        /// This method is used to edit a reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns>Reservation</returns>
        public Reservation EditReservation(Reservation reservation)
        {
            reservation.CheckOut = reservation.CheckOut.Date;
            reservation.CheckIn = reservation.CheckIn.Date;
            var reservations = _resrepo.GetAll();
            foreach (var item in reservations)
            {
                if (item.RoomId == reservation.RoomId && item.HotelId == reservation.HotelId)
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
            return _resrepo.Update(reservation);
        }

        /// <summary>
        /// This method is used to get all the bookef Rooms
        /// </summary>
        /// <param name="hotelDTO"></param>
        /// <returns>List of RoomDTO</returns>
        public List<RoomDTO> GetAllBookedRoom(HotelDTO hotelDTO)
        {
            var reservations = _resrepo.GetAll();
            List<RoomDTO> rooms = new List<RoomDTO>();
            foreach (var item in reservations)
            {
                if (item.HotelId == hotelDTO.Id && item.CheckIn <= hotelDTO.BookedDate.Date && item.CheckOut> hotelDTO.BookedDate.Date)
                {
                    RoomDTO room = new RoomDTO();
                    room.Id = item.RoomId;
                    rooms.Add(room);

                }
            }
            return rooms;
        }

        /// <summary>
        /// This method is used to get all the reservation of a user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>UserDTO</returns>
        public List<Reservation> GetAllReservation(UserDTO userDTO)
        {
            return _resrepo.GetAll().Where(r=>r.Username == userDTO.Username).ToList();
        }


        /// <summary>
        /// This method is used to get the count of booked room for all the hotels
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List of HotelCountDTO</returns>
        public List<HotelCountDTO> GetCountOfBookedRoomForAllHotel(DateDTO date)
        {
            List<HotelCountDTO> hotelCountDTOs = new List<HotelCountDTO>();
            var hotels = _resrepo.GetAll().Select(r=>r.HotelId).Distinct();
            foreach (var item in hotels)
            {
                 hotelCountDTOs.Add(GetCountOfBookedRoomForHotel(new HotelDTO { Id=item,BookedDate=date.BookedDate }));   
            }
            return hotelCountDTOs;
        }

        /// <summary>
        /// This method is used to get the count of booked room for a hotel
        /// </summary>
        /// <param name="hotelDTO"></param>
        /// <returns>HotelCountDTO</returns>
        public HotelCountDTO GetCountOfBookedRoomForHotel(HotelDTO hotelDTO)
        {
            return new HotelCountDTO { HotelId=hotelDTO.Id, BookedRoomcount=GetAllBookedRoom(hotelDTO).Count };
        }
    }
}
