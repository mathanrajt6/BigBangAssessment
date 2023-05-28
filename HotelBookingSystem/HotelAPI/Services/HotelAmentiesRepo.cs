using HotelAPI.Interfaces;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class HotelAmentiesRepo : IRepo<HotelAmenity, int>
    {
        private readonly HotelContext _context;

        /// <summary>
        /// This method is used to inject the dependencies
        /// </summary>
        /// <param name="context"></param>
        public HotelAmentiesRepo(HotelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method adds a hotel amenity to the database if it does not already exist.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>HotelAmenity</returns>
        public HotelAmenity Add(HotelAmenity item)
        {
            var hotelAmenity = _context.HotelAmenities.SingleOrDefault(ha=>ha.HotelId == item.HotelId && ha.AmentityId == item.AmentityId);
            if(hotelAmenity == null)
            {
                _context.HotelAmenities.Add(item);
                _context.SaveChanges();
            }
            return item;
        }

        /// <summary>
        /// This method deletes a hotel amenity from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HotelAmenity</returns>
        public HotelAmenity Delete(int id)
        {
            var hotelAmenity = _context.HotelAmenities.SingleOrDefault(ha=>ha.HotelAmentityId==id);
            if(hotelAmenity != null)
            {
                _context.HotelAmenities.Remove(hotelAmenity);
                _context.SaveChanges();
            }
            return hotelAmenity;
        }


        /// <summary>
        /// This method gets a hotel amenity from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HotelAmenity</returns>
        public HotelAmenity Get(int id)
        {
            return _context.HotelAmenities.SingleOrDefault(ha=>ha.HotelId == id);
        }

        /// <summary>
        /// This method gets all hotel amenities from the database.
        /// </summary>
        /// <returns>List of HotelAmenity</returns>
        public ICollection<HotelAmenity> GetAll()
        {
            return _context.HotelAmenities.ToList();
        }


        /// <summary>
        /// This method updates a hotel amenity in the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>HotelAmenity</returns>
        public HotelAmenity Update(HotelAmenity item)
        {
            var hotelAmenity = _context.HotelAmenities.SingleOrDefault(ha=>ha.HotelId == item.HotelId && ha.AmentityId == item.AmentityId);
            if(hotelAmenity != null)
            {
                hotelAmenity.HotelId = item.HotelId;
                hotelAmenity.AmentityId = item.AmentityId;
                _context.SaveChanges();
            }
            return hotelAmenity;
        }
    }
}
