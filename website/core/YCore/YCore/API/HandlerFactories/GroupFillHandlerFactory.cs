using System.Net;
using YApiModel.Models;
using YCore.API.Handlers;

namespace YCore.API.HandlerFactories
{
    public class GroupFillHandlerFactory : HandlerFactory, IHandlerFactory
    {
        public GroupFillHandlerFactory(HttpListenerContext context) : base(context)
        {
        }

        public IHandler GetHandler()
        {
            if (!TokenValidated(GetToken()))
            {
                return new TokenAuthtorizationFailed();
            }
            var fillData = GetData<List<GroupFillData>>();
            if (fillData == null)
            {
                return new DataExpected();
            }
            return new GroupFillHandler(fillData);
        }
    }
}
