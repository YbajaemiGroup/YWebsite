using System.Net;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    internal class TokenCreateHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public TokenCreateHandlerFactory(HttpListenerContext context) : base(context)
        {
        }

        public IHandler GetHandler()
        {
            string token = GetParameter("token");
            if (!TokenValidated(token))
            {
                return new TokenAuthtorizationFailed();
            }
            string key = GetParameter("token_source");
            return new TokenCreateHandler(key);
        }
    }
}
