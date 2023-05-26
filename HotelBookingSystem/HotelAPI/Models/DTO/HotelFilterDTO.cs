namespace HotelAPI.Models.DTO
{
    public class HotelFilterDTO
    {
        public string? Country { get; set; }
        public string? City { get; set; }

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

    }
}
