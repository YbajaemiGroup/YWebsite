using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YApiModel.Models;
using YCore.API.Handlers;
using YCore.Data;

namespace YCore.API.HandlerFactories
{
    public class LinksAddHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public LinksAddHandlerFactory(HttpListenerContext context) : base(context)
        {
            this.context = context;
        }

        public IHandler GetHandler()
        {
            if (!TokenValidated(GetToken()))
            {
                return new TokenAuthtorizationFailed();
            }
            var links = GetData<List<Link>>();
            if (links == null)
            {
                return new DataExpected();
            }
            return new LinksAddHandler(links);
        }
    }
}
