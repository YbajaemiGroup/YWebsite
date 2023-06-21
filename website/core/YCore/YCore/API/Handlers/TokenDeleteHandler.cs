using YCore.API.IO;
using YCore.Data;

namespace YCore.API.Handlers
{
    internal class TokenDeleteHandler : Handler, IHandler
    {
        // token need to delete
        private readonly string _token;

        public TokenDeleteHandler(string token)
        {
            _token = token;
        }

        public Response ProcessRequest()
        {
            DatabaseInteractor.Instance().DeleteToken(_token);
            return new() { ResponseData = null };
        }
    }
}
