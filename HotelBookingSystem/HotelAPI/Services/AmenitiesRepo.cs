using HotelAPI.Interfaces;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class AmenitiesRepo : IRepo<Amenity, int>
    {
        private readonly HotelContext _context;

        /// <summary>
        /// This method is used to inject the dependencies
        /// </summary>
        /// <param name="context"></param>
        public AmenitiesRepo(HotelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method adds an amenity to the database if it does not already exist.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Amenity</returns>
        public Amenity Add(Amenity item)
        {
            var amenity = _context.Amenities.SingleOrDefault(a=>a.Name==item.Name);
            if(amenity == null)
            {
                _context.Amenities.Add(item);
                _context.SaveChanges();
            }
            return item;
        }

        /// <summary>
        /// This method deletes an amenity from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Amenity</returns>
        public Amenity Delete(int id)
        {
            var amenity = _context.Amenities.SingleOrDefault(a=>a.AmentityId == id);
            if(amenity != null)
            {
                _context.Amenities.Remove(amenity);
                _context.SaveChanges();
            }
            return amenity;
        }

        /// <summary>
        /// This method gets an amenity from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Amenity</returns>
        public Amenity Get(int id)
        {
            return _context.Amenities.SingleOrDefault(a=>a.AmentityId == id);
        }

        /// <summary>
        /// This method gets all amenities from the database.
        /// </summary>
        /// <returns>List of Amenity</returns>
        public ICollection<Amenity> GetAll()
        {
            return _context.Amenities.ToList();
        }

        /// <summary>
        /// This method updates an amenity in the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Amenity</returns>
        public Amenity Update(Amenity item)
        {
            var amenity = _context.Amenities.SingleOrDefault(a=>a.AmentityId == item.AmentityId);
            if(amenity != null)
            {
                amenity.Description = item.Description;
                _context.SaveChanges();
            }
            return amenity;
        }
    }
}
