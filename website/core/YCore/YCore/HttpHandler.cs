using System.Net;
using YCore.API;
using YCore.API.IO;
using YCore.Data;
using YCore.Data.OS;

namespace YCore
{
    public class HttpHandler
    {
        private readonly string _frontendLocation;
        private readonly List<string> _urls;

        public HttpHandler(Configuration configuration)
        {
            _frontendLocation = configuration.FrontendLocation;
            _urls = configuration.HttpListenAddresses;
        }

        private bool IsApi(string url)
        {
            url = url.Split('?').First();
            return ApiHandler.METHODS.Contains(url.Split('/')[^1]);
        }

        public void ExecuteHandler(HttpListenerContext context)
        {
            string url = context.Request.RawUrl ?? string.Empty;
            if (IsApi(url))
                return;
            var fileName = url[1..];
            if (fileName == null)
            {
                context.Response.Close();
                return;
            }

            Logger.Log(LogSeverity.Debug, nameof(HttpHandler), fileName);

            if (!SecurityUtilities.ValidateFileName(fileName))
            {
                context.Response.StatusCode = 403;
                context.Response.Close();
                return;
            }

            var filesOperator = new FilesOperator(_frontendLocation);
            var fileStream = filesOperator.GetFile(fileName);
            if (fileStream == null)
            {
                context.Response.StatusCode = 404;
                context.Response.OutputStream.Flush();
                return;
            }
            var responseSender = new RawDataResponseSender(FilesOperator.GetFileExtention(fileName), fileStream);
            responseSender.Send(context.Response);
            fileStream.Dispose();
            context.Response.Close();
        }
    }
}
