using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class GroupGamesGetHandlerFactory : IHandlerFactory
    {
        public IHandler GetHandler()
        {
            return new GroupGamesGetHandler();
        }
    }
}
