using System.Net;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class LinksGetHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public LinksGetHandlerFactory(HttpListenerContext context) : base(context)
        {
        }

        public IHandler GetHandler()
        {
            int playerId;
            try
            {
                playerId = Convert.ToInt32(GetParameter("player_id"));
            }
            catch (ArgumentNullException)
            {
                return new LinksGetHandler();
            }
            catch (Exception)
            {
                return new InvalidParameter("player_id");
            }
            return new LinksGetHandler(playerId);
        }
    }
}
