using System.Net;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    internal class TokenDeleteHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public TokenDeleteHandlerFactory(HttpListenerContext context) : base(context)
        {
        }

        public IHandler GetHandler()
        {
            string token = GetParameter("token");
            if (string.IsNullOrEmpty(token) || !TokenValidated(token))
            {
                return new TokenAuthtorizationFailed();
            }
            string d_token = GetParameter("d_token");
            return new TokenDeleteHandler(d_token);
        }
    }
}
