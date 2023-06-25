using YCore.API.IO;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    internal class TokenAuthtorizationFailed : IHandler
    {
        public IResponseSender GetResponseSender()
        {
            return new JsonResponseSender(new()
            {
                Exception = new TokenAuthentificationFailedException()
            });
        }
    }
}
