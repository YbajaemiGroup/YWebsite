using YApiModel;
using YCore.API.IO;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    public abstract class Handler
    {
        protected CoreException? CoreException;

        protected Response GetResponse(object? responseData)
        {
            if (CoreException == null)
            {
                return new() { ResponseData = responseData };
            }
            return new() { Exception = CoreException };
        }

        protected JsonResponseSender GetResponseSender(object? responseData)
        {
            return new JsonResponseSender(GetResponse(responseData));
        }
    }
}
