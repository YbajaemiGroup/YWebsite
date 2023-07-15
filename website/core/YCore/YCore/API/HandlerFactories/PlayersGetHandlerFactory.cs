using System.Net;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class PlayersGetHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public PlayersGetHandlerFactory(HttpListenerContext context) : base(context)
        {
        }

        public IHandler GetHandler()
        {
            int? playerId;
            try
            {
                playerId = Convert.ToInt32(GetParameter("player_id"));
            }
            catch (Exception)
            {
                playerId = null;
            }
            return new PlayersGetHandler(playerId);
        }
    }
}
