using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Models
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reservation>().HasData(
           new Reservation
               {
                   Id = 1,
                   Username = "mathan",
                   HotelId = 1,
                   RoomId = 101,
                   CheckIn = new DateTime(2023, 5, 26).Date,
                   CheckOut = new DateTime(2023, 5, 28).Date,
                   ReservationDate = DateTime.Now
               },
            new Reservation
               {
                   Id = 2,
                   Username = "raj",
                   HotelId = 2,
                   RoomId = 201,
                   CheckIn = new DateTime(2023, 6, 10).Date,
                   CheckOut = new DateTime(2023, 6, 15).Date,
                   ReservationDate = DateTime.Now
               },
            new Reservation
                   {
                       Id = 3,
                       Username = "kishore",
                       HotelId = 1,
                       RoomId = 102,
                       CheckIn = new DateTime(2023, 7, 1).Date,
                       CheckOut = new DateTime(2023, 7, 5).Date,
                       ReservationDate = DateTime.Now
                   },
               new Reservation
                   {
                       Id = 4,
                       Username = "gokulan",
                       HotelId = 3,
                       RoomId = 301,
                       CheckIn = new DateTime(2023, 8, 15).Date,
                       CheckOut = new DateTime(2023, 8, 20).Date,
                       ReservationDate = DateTime.Now
                   }
                );
        }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
