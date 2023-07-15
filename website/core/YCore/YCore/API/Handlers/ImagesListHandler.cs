using YApiModel.Models;
using YCore.API.IO;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class ImagesListHandler : IHandler
    {
        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            return new JsonResponseSender(new YApiModel.Response()
            {
                ResponseData = db.GetPlayersImages().Select(i => new Image(i.Id, i.ImageName))
            });
        }
    }
}
