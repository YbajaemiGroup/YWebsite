using System.Net;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class PlayersDeleteHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public PlayersDeleteHandlerFactory(HttpListenerContext context) : base(context)
        {
        }

        public IHandler GetHandler()
        {
            if (!TokenValidated(GetToken()))
            {
                return new TokenAuthtorizationFailed();
            }
            try
            {
                int playerId = Convert.ToInt32(GetParameter("player_id"));
                return new PlayersDeleteHandler(playerId);
            }
            catch (ArgumentNullException)
            {
                return new InvalidParameter("player_id", "Parameter expected.");
            }
        }
    }
}
