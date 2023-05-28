namespace UserAPI.Models
{
    public class Error
    {
        public int errorNumber { get; set; }
        public string errorMessage { get; set; }
        public Error()
        {

        }

        public Error(int errorNumber, string errorMessage)
        {
            this.errorNumber = errorNumber;
            this.errorMessage = errorMessage;
        }
    }
}
