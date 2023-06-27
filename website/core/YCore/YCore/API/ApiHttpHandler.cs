using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YCore.API.HandlerFactories;
using YCore.Data;

namespace YCore.API
{
    internal class ApiHttpHandler
    {
        private Configuration configuration;

        public ApiHttpHandler(Configuration configuration)
        {
            this.configuration = configuration;
        }

        private string ExtractMethod(string url)
        {
            return url.Split("?")[0].Split("/")[^1];
        }

        private IHandlerFactory GetHandlerFactory(string method, HttpListenerContext context)
        {
            return method switch
            {
                "token.create" => new TokenCreateHandlerFactory(context),
                "token.delete" => new TokenDeleteHandlerFactory(context),
                "bracket.updates.set" => new BracketSetUpdatesHandlerFactory(context),
                "bracket.updates.get" => new BracketGetUpdatesHandlerFactory(),
                "links.get" => new LinksGetHandlerFactory(context),
                "links.add" => new LinksAddHandlerFactory(context),
                "links.delete" => new LinksDeleteHandlerFactory(context),
                "images.load" => new ImagesLoadHandlerFactory(context, configuration.ImagesLocation),
                "images.get" => new ImagesGetHandlerFactory(context, configuration.ImagesLocation, configuration.StaffImagesLocation),
                "group.fill" => new GroupFillHandlerFactory(context),
                "group.games.get" => new GroupGamesGetHandlerFactory(),
                "players.add" => new PlayersAddHandlerFactory(context),
                "players.get" => new PlayersGetHandlerFactory(context),
                "players.delete" => new PlayersDeleteHandlerFactory(context),
                _ => throw new ArgumentOutOfRangeException(nameof(method))
            };
        }

        public void ExecuteHandler(HttpListenerContext context)
        {
            string method = ExtractMethod(context.Request.RawUrl ?? string.Empty);
            IHandlerFactory handlerFactory;
            try
            {
                handlerFactory = GetHandlerFactory(method, context);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            IHandler handler = handlerFactory.GetHandler();
            IResponseSender responseSender = handler.GetResponseSender();
            responseSender.Send(context.Response.OutputStream);
        }
    }
}
