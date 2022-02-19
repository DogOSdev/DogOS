namespace DogOS.Types.Errors
{
    public class UnknownError : Error
    {
        public UnknownError(string details) : base("Unknown Error", details)
        {
        }
    }
}