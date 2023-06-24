using System.Net;
using YApiModel.Models;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories;

internal class BracketSetUpdatesHandlerFactory : HandlerFactory, IHandlerFactory
{
    private readonly List<Round>? bracket;

    public BracketSetUpdatesHandlerFactory(HttpListenerContext context) : base(context)
    {
        bracket = GetData<List<Round>>(context);
    }

    public IHandler GetHandler()
    {
        string token = GetParameter("token");
        if (!TokenValidated(token))
        {
            return new TokenAuthtorizationFailed();
        }
        if (bracket == null)
        {
            return new DataExpected();
        }
        return new BracketSetUpdatesHandler(bracket);
    }
}
