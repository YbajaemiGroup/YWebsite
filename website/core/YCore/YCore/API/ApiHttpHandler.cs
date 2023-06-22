using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YCore.API.HandlerFactories;

namespace YCore.API
{
    internal class ApiHttpHandler
    {
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
            catch (ArgumentOutOfRangeException e)
            {
                Logger.Log(LogSeverity.Warning, nameof(HttpHandler), "Cant get handler factory", e);
                return;
            }
            //            Response response = new() { Exception = CoreException.UnknownException };
            try
            {
                IHandler handler = handlerFactory.GetHandler();
                //response = handler.ProcessRequest();
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Error, nameof(HttpHandler), "Error", e);
            }
            //JsonSerializer.Serialize(context.Response.OutputStream, response);
            context.Response.OutputStream.Close();
        }
    }
}
