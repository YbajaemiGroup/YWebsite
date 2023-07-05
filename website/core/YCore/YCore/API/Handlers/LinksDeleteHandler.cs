using YApiModel;
using YCore.API.IO.Exceptions;
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

        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            if (!db.DeleteLink(linkId).Result)
            {
                CoreException = new UnknownInnerException();
            }
            return GetResponseSender(null);
        }
    }
}
