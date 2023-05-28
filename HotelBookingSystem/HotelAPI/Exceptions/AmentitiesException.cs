namespace HotelAPI.Exceptions
{
    public class AmentitiesException : Exception
    {
        public string ExceptionMessage { get; set; }
        public AmentitiesException()
        {
            ExceptionMessage = "Amentities Exception";
        }
        public AmentitiesException(string message)
        {
            ExceptionMessage = message;
        }

        public override string Message => ExceptionMessage;
    }
}
