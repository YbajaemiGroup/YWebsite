using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class PlayersAddHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public PlayersAddHandlerFactory(HttpListenerContext context) : base(context)
        {
        }

        public IHandler GetHandler()
        {
            if (!TokenValidated(GetToken()))
            {
                return new TokenAuthtorizationFailed();
            }
            var players = GetData<List<YApiModel.Models.Player>>();
            if (players == null)
            {
                return new DataExpected();
            }
            throw new NotImplementedException();
        }
    }
}
