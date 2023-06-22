namespace YCore.API.IO.Exceptions
{
    internal class InvalidParameterException : CoreException
    {
        private const int CODE = 3;
        private const string MESSAGE = "Invalid paramter type or value.";

        public InvalidParameterException() 
            : base(false, CODE, MESSAGE)
        {
        }
    }
}
