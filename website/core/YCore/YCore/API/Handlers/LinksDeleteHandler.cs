using YApiModel;
using YCore.Data;

namespace YCore.API.Handlers
{
    internal class LinksDeleteHandler : Handler, IHandler
    {
        private readonly int linkId;

        public LinksDeleteHandler(int linkId)
        {
            this.linkId = linkId;
        }

        public Response ProcessRequest()
        {
            var db = DatabaseInteractor.Instance();
            db.DeleteLink(linkId);
            return GetResponse(null);
        }
    }
}
