using YApiModel;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    public class InvalidParameter : IHandler
    {
        private readonly string? parameterName;

        public InvalidParameter()
        {
        }

        public InvalidParameter(string parameterName)
        {
            this.parameterName = parameterName;
        }

        public Response ProcessRequest()
        {
            if (parameterName != null)
            {
                return new()
                {
                    Exception = new InvalidParameterException(parameterName)
                };
            }
            return new()
            {
                Exception = new InvalidParameterException()
            };
        }
    }
}
