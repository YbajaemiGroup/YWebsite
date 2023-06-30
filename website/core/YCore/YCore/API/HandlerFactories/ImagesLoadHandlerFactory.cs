using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Net;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class ImagesLoadHandlerFactory : HandlerFactory, IHandlerFactory
    {
        private string imageName = string.Empty;
        private readonly string imagesLocation;

        public ImagesLoadHandlerFactory(HttpListenerContext context, string imagesLocation) 
            : base(context)
        {
            this.imagesLocation = imagesLocation;
        }

        public IHandler GetHandler()
        {
            if (!TokenValidated(GetToken()))
            {
                return new TokenAuthtorizationFailed();
            }
            try
            {
                imageName = GetParameter("image_name");
            }
            catch (ArgumentNullException)
            {
                return new InvalidParameter("image_name", "No parameter found.");
            }
            catch (Exception ex)
            {
                Logger.Log(LogSeverity.Warning, nameof(ImagesLoadHandlerFactory), "Unknown error occured while id parsing.", ex);
            }
            if (context.Request.ContentLength64 == 0)
            {
                return new DataExpected();
            }
            var ms = new MemoryStream();
            context.Request.InputStream.CopyTo(ms);
            var offset = ms.Length - context.Request.ContentLength64;

            return new ImagesLoadHandler(imageName, imagesLocation, ms, offset);
        }
    }
}
