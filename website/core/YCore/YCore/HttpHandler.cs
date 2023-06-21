using System.Net;
using YCore.API;

namespace YCore
{
#warning это класс должен отвечать за определение, api это или нет и вызывать нужный обработчик. Код, что тут есть должен быть полностью переделан
    internal class HttpHandler
    {
        private string ExtractMethod(string url)
        {
            return url.Split("?")[0].Split("/")[^1];
        }

        private IHandlerFactory GetHandlerFactory(string method)
        {
            return method switch
            {
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void ExecuteHandler(HttpListenerContext context)
        {
            string method = ExtractMethod(context.Request.RawUrl ?? string.Empty);
            IHandlerFactory handlerFactory;
            try
            {
                handlerFactory = GetHandlerFactory(method);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Logger.Log(LogSeverity.Warning, nameof(HttpHandler), "Cant get handler factory", e);
                return;
            }
//            Response response = new() { Exception = CoreException.UnknownException };
            try
            {
                //IHandler handler = handlerFactory.Create(context);
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
