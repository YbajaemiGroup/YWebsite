using YCore.API.IO;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    internal class DataExpected : IHandler
    {
        public IResponseSender GetResponseSender()
        {
            return new JsonResponseSender(new()
            {
                Exception = new DataExpectedException()
            });
        }
    }
}
