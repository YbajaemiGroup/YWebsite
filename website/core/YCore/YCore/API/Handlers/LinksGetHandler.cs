using YApiModel.Models;
using YCore.Data;
using YDatabase.Models;

namespace YCore.API.Handlers
{
    public class LinksGetHandler : Handler, IHandler
    {
        private readonly int playerId;
        private readonly bool playerIdSetted;

        public LinksGetHandler()
        {
            playerIdSetted = false;
        }

        public LinksGetHandler(int playerId)
        {
            this.playerId = playerId;
            playerIdSetted = true;
        }

        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            List<YDatabase.Models.Link> links = db.GetLinks();
            if (playerIdSetted)
            {
                links.RemoveAll(l => l.Player != playerId);
            }
            return GetResponseSender(links.Select(l => new YApiModel.Models.Link(l.Link1, l.Descr, l.Id, playerId)));
        }
    }
}
