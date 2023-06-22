using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    internal class BracketGetUpdatesHandlerFactory : IHandlerFactory
    {
        public IHandler GetHandler()
        {
            return new BracketGetUpdatesHandler();
        }
    }
}
