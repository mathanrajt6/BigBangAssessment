using HotelAPI.Interfaces;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class RoomRepo : IRepo<Room, int>
    {
        private readonly HotelContext _context;

        /// <summary>
        /// This method is used to inject the dependencies
        /// </summary>
        /// <param name="context"></param>
        public RoomRepo(HotelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method adds a room to the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Room</returns>
        public Room Add(Room item)
        {
            var room = _context.Rooms.SingleOrDefault(r=>r.RoomId == item.RoomId);
            if(room == null)
            {
                _context.Rooms.Add(item);
                _context.SaveChanges();
            }
            return item;

        }

        /// <summary>
        /// This method deletes a room from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Room</returns>
        public Room Delete(int id)
        {
            var room = _context.Rooms.SingleOrDefault(r=>r.RoomId==id);
            if(room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
            return room;
        }

        /// <summary>
        /// This method gets a room from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Room</returns>
        public Room Get(int id)
        {
            return _context.Rooms.SingleOrDefault(r=>r.RoomId == id);
        }


        /// <summary>
        /// This method gets all rooms from the database.
        /// </summary>
        /// <returns>List of Room</returns>
        public ICollection<Room> GetAll()
        {
            return _context.Rooms.ToList();
        }

        /// <summary>
        /// This method updates a room in the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Room</returns>
        public Room Update(Room item)
        {
            var room = _context.Rooms.SingleOrDefault(r=>r.RoomId == item.RoomId && r.HotelId == item.HotelId);
            if(room != null)
            {
                room.RoomNumber = item.RoomNumber;
                room.Capacity = item.Capacity;
                room.Price = item.Price;
                room.AC = item.AC;
                _context.SaveChanges();
            }
            return room;
        }
    }
}
