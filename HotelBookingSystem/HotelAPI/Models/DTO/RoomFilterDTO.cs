namespace HotelAPI.Models.DTO
{
    public class RoomFilterDTO
    {
        public int? HotelId { get; set; }
        public int? Capacity { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

    }
}
