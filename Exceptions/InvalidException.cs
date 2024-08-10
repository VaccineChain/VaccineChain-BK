namespace vaccine_chain_bk.Exceptions
{
    public class InvalidException : Exception
    {
        public InvalidException(string message) : base(message)
        {
        }

        public InvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
