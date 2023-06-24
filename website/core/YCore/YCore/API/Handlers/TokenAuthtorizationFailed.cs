using YApiModel;
using YCore.API.IO;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    internal class TokenAuthtorizationFailed : IHandler
    {
        public Response ProcessRequest()
        {
            return new()
            {
                Exception = new TokenAuthentificationFailedException()
            };
        }
    }
}
