using YApiModel;
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
    }
}
