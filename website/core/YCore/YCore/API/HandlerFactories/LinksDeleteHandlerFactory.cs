using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class LinksDeleteHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public LinksDeleteHandlerFactory(HttpListenerContext context) : base(context)
        {
        }

        public IHandler GetHandler()
        {
            if (!TokenValidated(GetToken()))
            {
                return new TokenAuthtorizationFailed();
            }
            int linkId;
            try
            {
                linkId = Convert.ToInt32(GetParameter("link_id"));
            }
            catch (Exception ex)
            {
                Logger.Log(LogSeverity.Info, nameof(LinksDeleteHandlerFactory), "Error occured while parsing link id.", ex);
                return new InvalidParameter("link_id");
            }
            return new LinksDeleteHandler(linkId);
        }
    }
}
