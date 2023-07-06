using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class GroupGetHandlerFactory : IHandlerFactory
    {
        public IHandler GetHandler()
        {
            return new GroupGetHandler();
        }
    }
}
