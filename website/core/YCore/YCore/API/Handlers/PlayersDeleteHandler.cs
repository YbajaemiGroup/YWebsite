using YApiModel.Models;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class PlayersDeleteHandler : Handler, IHandler
    {
        private readonly int player_id;

        public PlayersDeleteHandler(int player_id)
        {
            this.player_id = player_id;
        }

        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            var p = db.GetPlayer(player_id);
            var player = new Player(
                p.Nickname,
                p.Descr ?? string.Empty,
                p.Image?.ImageName,
                p.Id,
                p.GroupNumber,
                p.Won,
                p.Lose,
                p.Points);
            db.DeleteGames(p.GamePlayer1Navigations.Concat(p.GamePlayer2Navigations)).Wait();
            db.DeletePlayer(p);
            return GetResponseSender(player);
        }
    }
}
