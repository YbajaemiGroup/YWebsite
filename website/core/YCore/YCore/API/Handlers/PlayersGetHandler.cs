using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YApiModel.Models;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class PlayersGetHandler : Handler, IHandler
    {
        private int? playerId;

        public PlayersGetHandler(int? playerId)
        {
            this.playerId = playerId;
        }

        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            if (playerId == null)
            {
                var players = db.GetPlayers().Select(p => new Player(
                    p.Nickname,
                    p.Descr ?? string.Empty,
                    p.Image?.ImageName,
                    p.Id,
                    p.GroupNumber,
                    p.Won,
                    p.Lose,
                    p.Points));
                return GetResponseSender(players);
            }
            else
            {
                var player = db.GetPlayer(playerId.Value);
                return GetResponseSender(new Player(
                    player.Nickname,
                    player.Descr ?? string.Empty,
                    player.Image?.ImageName,
                    player.Id,
                    player.GroupNumber,
                    player.Won,
                    player.Lose,
                    player.Points));
            }
        }
    }
}
