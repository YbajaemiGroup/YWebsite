using YApiModel;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    public class InvalidParameter : IHandler
    {
        public Response ProcessRequest()
        {
            return new()
            {
                Exception = new InvalidParameterException()
            };
        }
    }
}
