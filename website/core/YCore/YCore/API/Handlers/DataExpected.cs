using YApiModel;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    internal class DataExpected : IHandler
    {
        public Response ProcessRequest()
        {
            return new()
            {
                Exception = new DataExpectedException()
            };
        }
    }
}
