using System.Net;
using YCore.API.HandlerFactories;
using YCore.Data;

namespace YCore.API
{
    public class ApiHandler
    {
        public const string TOKEN_CREATE = "token.create";
        public const string TOKEN_DELETE = "token.delete";
        public const string BRACKET_UPDATES_GET = "bracket.updates.get";
        public const string BRACKET_UPDATES_SET = "bracket.updates.set";
        public const string LINKS_GET = "links.get";
        public const string LINKS_ADD = "links.add";
        public const string LINKS_DELETE = "links.delete";
        public const string IMAGES_LOAD = "images.load";
        public const string IMAGES_LIST = "images.list";
        public const string IMAGES_GET = "images.get";
        public const string GROUP_FILL = "group.fill";
        public const string GROUP_GET = "group.get";
        public const string PLAYERS_ADD = "players.add";
        public const string PLAYERS_GET = "players.get";
        public const string PLAYERS_DELETE = "players.delete";

        public static List<string> METHODS { get; } = new()
        {
            TOKEN_CREATE,
            TOKEN_DELETE,
            BRACKET_UPDATES_GET,
            BRACKET_UPDATES_SET,
            LINKS_ADD,
            LINKS_DELETE,
            LINKS_GET,
            IMAGES_GET,
            IMAGES_LIST,
            IMAGES_LOAD,
            GROUP_FILL,
            GROUP_GET,
            PLAYERS_ADD,
            PLAYERS_GET,
            PLAYERS_DELETE
        };

        private readonly Configuration configuration;

        public ApiHandler(Configuration configuration)
        {
            this.configuration = configuration;
        }

        private static string ExtractMethod(string url)
        {
            return url.Split("?")[0].Split("/")[^1];
        }

        private IHandlerFactory? GetHandlerFactory(string method, HttpListenerContext context)
        {
            return method switch
            {
                TOKEN_CREATE => new TokenCreateHandlerFactory(context),
                TOKEN_DELETE => new TokenDeleteHandlerFactory(context),
                BRACKET_UPDATES_SET => new BracketSetUpdatesHandlerFactory(context),
                BRACKET_UPDATES_GET => new BracketGetUpdatesHandlerFactory(),
                LINKS_GET => new LinksGetHandlerFactory(context),
                LINKS_ADD => new LinksAddHandlerFactory(context),
                LINKS_DELETE => new LinksDeleteHandlerFactory(context),
                IMAGES_LOAD => new ImagesLoadHandlerFactory(context, configuration.ImagesLocation),
                IMAGES_GET => new ImagesGetHandlerFactory(context, configuration.ImagesLocation, configuration.StaffImagesLocation),
                IMAGES_LIST => new ImagesListHandlerFactory(context),
                GROUP_FILL => new GroupFillHandlerFactory(context),
                GROUP_GET => new GroupGetHandlerFactory(),
                PLAYERS_ADD => new PlayersAddHandlerFactory(context),
                PLAYERS_GET => new PlayersGetHandlerFactory(context),
                PLAYERS_DELETE => new PlayersDeleteHandlerFactory(context),
                _ => null
            };
        }

        public void ExecuteHandler(HttpListenerContext context)
        {
            string method = ExtractMethod(context.Request.RawUrl ?? string.Empty);
            IHandlerFactory? handlerFactory = GetHandlerFactory(method, context);
            if (handlerFactory == null)
                return;
            IHandler handler = handlerFactory.GetHandler();
            IResponseSender responseSender = handler.GetResponseSender();
            responseSender.Send(context.Response.OutputStream);
            context.Response.OutputStream.Close();
        }
    }
}
