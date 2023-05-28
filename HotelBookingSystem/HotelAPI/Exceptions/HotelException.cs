namespace HotelAPI.Exceptions
{
    public class HotelException : Exception
    {
        public string ExceptionMessage { get; set; }
        public HotelException()
        {
            ExceptionMessage = "Hotel Amentities Exception";
        }
        public HotelException(string message)
        {
            ExceptionMessage = message;
        }

        public override string Message => ExceptionMessage;
    }
}
