using YApiModel;
using YCore.API.IO.Exceptions;
using YCore.Data;
using YCore.Data.OS;
using YDatabase.Models;

namespace YCore.API.Handlers
{
    public class ImagesLoadHandler : Handler, IHandler
    {
        private readonly string imageName;
        private readonly ImagesOperator imagesOperator;
        private readonly byte[] imageData;

        public ImagesLoadHandler(string imageName, string imagesLocation, byte[] imageData)
        {
            this.imageName = imageName;
            imagesOperator = new(imagesLocation);
            this.imageData = imageData;
        }

        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            Image image = new()
            {
                ImageName = imageName,
                IsStaff = false
            };
            image = db.InsertImage(image).Result;
            if (imagesOperator.SaveImage(imageName, imageData))
            {
                CoreException = new UnknownInnerException();
                Logger.Log(LogSeverity.Info, nameof(ImagesLoadHandler), "Can't save image to disk.");
                return GetResponseSender(null);
            }
            return GetResponseSender(new YApiModel.Models.Image(image.Id, image.ImageName));
        }
    }
}
