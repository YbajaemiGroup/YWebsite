﻿using YCore.API.IO.Exceptions;
using YCore.Data;
using YCore.Data.OS;
using YDatabase.Models;

namespace YCore.API.Handlers
{
    public class ImagesLoadHandler : Handler, IHandler
    {
        private readonly string imageName;
        private readonly FilesOperator imagesOperator;
        private readonly Stream imageStream;

        public ImagesLoadHandler(string imageName, string imagesLocation, Stream imageStream)
        {
            this.imageName = imageName;
            imagesOperator = new(imagesLocation);
            this.imageStream = imageStream;
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
            if (!imagesOperator.SaveFile(imageName, imageStream))
            {
                CoreException = new UnknownInnerException();
                Logger.Log(LogSeverity.Info, nameof(ImagesLoadHandler), "Can't save image to disk.");
                return GetResponseSender(null);
            }
            return GetResponseSender(new YApiModel.Models.Image(image.Id, image.ImageName));
        }
    }
}
