using YApiModel;
using YCore.API.IO;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    public class InvalidParameter : IHandler
    {
        private readonly string? parameterName;
        private readonly string? message;

        public InvalidParameter()
        {
        }

        public InvalidParameter(string parameterName)
        {
            this.parameterName = parameterName;
        }

        public InvalidParameter(string parameterName, string message)
        {
            this.parameterName = parameterName;
            this.message = message;
        }

        public IResponseSender GetResponseSender()
        {
            if (parameterName != null)
            {
                if (message != null)
                {
                    return new JsonResponseSender(new()
                    {
                        Exception = new InvalidParameterException(parameterName, message)
                    });
                }
                return new JsonResponseSender(new()
                {
                    Exception = new InvalidParameterException(parameterName)
                });
            }
            return new JsonResponseSender(new()
            {
                Exception = new InvalidParameterException()
            });
        }
    }
}
