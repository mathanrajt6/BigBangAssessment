namespace HotelAPI.Exceptions
{
    public class HotelAmentitieesException : Exception
    {

        public string ExceptionMessage { get; set; }
        public HotelAmentitieesException()
        {
            ExceptionMessage = "Hotel Amentities Exception";
        }
        public HotelAmentitieesException(string message)
        {
            ExceptionMessage = message;
        }

        public override string Message => ExceptionMessage;
    }
}
