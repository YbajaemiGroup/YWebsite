﻿namespace YCore.API.IO.Exceptions
{
    internal class InvalidParameterException : CoreException
    {
        private const int CODE = 3;
        private const string MESSAGE = "Invalid paramter type or value.";

        public InvalidParameterException() 
            : base(false, CODE, MESSAGE)
        {
        }

        public InvalidParameterException(string parameterName)
            : base(false, CODE, $"{MESSAGE} Parameter: {parameterName}.")
        {
        }

        public InvalidParameterException(string parameterName, string message)
            : base(false, CODE, $"{MESSAGE} Parameter: {parameterName}. Message: {message}.")
        {
        }
    }
}
