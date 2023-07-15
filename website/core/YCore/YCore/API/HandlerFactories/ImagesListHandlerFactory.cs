using System.Net;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class ImagesListHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public ImagesListHandlerFactory(HttpListenerContext context) : base(context)
        {
        }

        public IHandler GetHandler()
        {
            return new ImagesListHandler();
        }
    }
}
