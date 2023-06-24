using YApiModel;
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

        public Response ProcessRequest()
        {
            var db = DatabaseInteractor.Instance();
            List<Link> links = db.GetLinks();
            if (playerIdSetted)
            {
                links.RemoveAll(l => l.Player != playerId);
            }
            return GetResponse(links);
        }
    }
}
