namespace UserAPI.Models
{
    public class Error
    {
        int errorNumber;
        string message;
        public Error()
        {

        }

        public Error(int errorNumber, string message)
        {
            this.errorNumber = errorNumber;
            this.message = message;
        }
    }
}
