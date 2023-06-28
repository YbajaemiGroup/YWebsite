using YCore.Data;

namespace YCore.API.Handlers
{
    public class GroupGamesGetHandler : Handler, IHandler
    {
        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            return GetResponseSender(db.GetGames());
        }
    }
}
