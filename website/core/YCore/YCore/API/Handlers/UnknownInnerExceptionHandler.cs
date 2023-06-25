using YApiModel;
using YCore.API.IO;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    public class UnknownInnerExceptionHandler : IHandler
    {
        public IResponseSender GetResponseSender()
        {
            return new JsonResponseSender(new Response()
            {
                Exception = new UnknownInnerException()
            });
        }
    }
}
