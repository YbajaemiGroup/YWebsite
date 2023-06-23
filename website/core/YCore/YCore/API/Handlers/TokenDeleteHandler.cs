using YApiModel;
using YCore.API.IO;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class TokenDeleteHandler : Handler, IHandler
    {
        // token need to delete
        private readonly string _token;

        public TokenDeleteHandler(string token)
        {
            _token = token;
        }

        public Response ProcessRequest()
        {
            return new() { ResponseData = DatabaseInteractor.Instance().DeleteToken(_token) };
        }
    }
}
