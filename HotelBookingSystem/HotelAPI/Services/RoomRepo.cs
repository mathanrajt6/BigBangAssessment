using HotelAPI.Interfaces;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class RoomRepo : IRepo<Room, int>
    {
        private readonly HotelContext _context;
        public RoomRepo(HotelContext context)
        {
            _context = context;
        }
        public Room Add(Room item)
        {
            var room = _context.Rooms.SingleOrDefault(r=>r.Id == item.Id);
            if(room == null)
            {
                _context.Rooms.Add(item);
                _context.SaveChanges();
            }
            return item;

        }

        public Room Delete(Room item)
        {
            var room = _context.Rooms.SingleOrDefault(r=>r.Id == item.Id && r.HotelId==item.HotelId);
            if(room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
            return room;
        }

        public Room Get(int id)
        {
            return _context.Rooms.SingleOrDefault(r=>r.Id == id);
        }

        public ICollection<Room> GetAll()
        {
            return _context.Rooms.ToList();
        }

        public Room Update(Room item)
        {
            var room = _context.Rooms.SingleOrDefault(r=>r.Id == item.Id && r.HotelId == item.HotelId);
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
