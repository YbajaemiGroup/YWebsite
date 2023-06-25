using System.Net;
using YCore.API.Handlers;
using YCore.Data.OS;

namespace YCore.API.HandlerFactories
{
    public class ImagesGetHandlerFactory : HandlerFactory, IHandlerFactory
    {
        private readonly string imagesLocation;
        private readonly string staffImagesLocation;

        public ImagesGetHandlerFactory(HttpListenerContext context, string imagesLocation, string staffImagesLocation) 
            : base(context)
        {
            this.imagesLocation = imagesLocation;
            this.staffImagesLocation = staffImagesLocation;
        }

        public IHandler GetHandler()
        {
            string imageName = GetParameter("image_name");
            string imageType;
            try
            {
                imageType = GetParameter("image_type");
            }
            catch (ArgumentNullException)
            {
                return new InvalidParameter("image_type", "Image type expected (players or resources).");
            }
            return imageType switch
            {
                "players" => new ImagesGetHandler(imageName, new ImagesOperator(imagesLocation)),
                "resources" => new ImagesGetHandler(imageName, new ImagesOperator(staffImagesLocation)),
                _ => new InvalidParameter("image_type", "Invalid value of parameter. Expected players or resources."),
            };
        }
    }
}
