using YApiModel;
using YCore.API.IO;
using YCore.API.IO.Exceptions;
using YCore.Data.OS;

namespace YCore.API.Handlers
{
    public class ImagesGetHandler : Handler, IHandler
    {
        private readonly string imageName;
        private readonly FilesOperator imagesOperator;

        public ImagesGetHandler(string imageName, FilesOperator imagesOperator)
        {
            this.imageName = imageName;
            this.imagesOperator = imagesOperator;
        }

        public IResponseSender GetResponseSender()
        {
            var imageData = imagesOperator.GetFile(imageName);
            if (imageData == null || !imageData.CanRead)
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
