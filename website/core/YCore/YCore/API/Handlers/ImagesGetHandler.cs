using YApiModel;
using YCore.API.IO;
using YCore.API.IO.Exceptions;
using YCore.Data.OS;

namespace YCore.API.Handlers
{
    public class ImagesGetHandler : Handler, IHandler
    {
        private readonly string imageName;
        private readonly ImagesOperator imagesOperator;

        public ImagesGetHandler(string imageName, ImagesOperator imagesOperator)
        {
            this.imageName = imageName;
            this.imagesOperator = imagesOperator;
        }

        public IResponseSender GetResponseSender()
        {
            var imageData = imagesOperator.GetImage(imageName);
            if (imageData == null)
            {
                return new JsonResponseSender(new Response()
                {
                    Exception = new UnknownInnerException()
                });
            }
            return new RawDataResponseSender(imageData);
        }
    }
}
