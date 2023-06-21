using System.Net;
using YCore.Data;

namespace YCore.API.HandlerFactories
{
    internal abstract class HandlerFactory
    {
        protected string parameters = string.Empty;

        protected HandlerFactory(HttpListenerContext context)
        {
            parameters = context.Request.RawUrl ?? string.Empty;
        }

        protected string GetParameter(string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName) || string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentNullException(nameof(parameterName));
            }
            var parametersList = parameters.Split("&");
            try
            {
                string p = parametersList.First(p => p.Split('=').First() == parameterName);
                return p.Split('=')[1];
            }
            catch (Exception)
            {
                Logger.Log(LogSeverity.Info, "HandlerFactory", $"Parameters parse failed. Parameter: {parameterName}.");
                return string.Empty;
            }
        }

        protected bool TokenValidated(string token)
        {
            return DatabaseInteractor.Instance().ValidateToken(token);
        }
    }
}
