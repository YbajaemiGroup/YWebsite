using System.Net;
using System.Text.Json;
using YApiModel;
using YCore.Data;

namespace YCore.API.HandlerFactories
{
    public abstract class HandlerFactory
    {
        protected HttpListenerContext context;
        protected string parameters = string.Empty;

        protected HandlerFactory(HttpListenerContext context)
        {
            this.context = context;
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
                throw new ArgumentNullException(nameof(parameterName));
            }
        }

        protected string GetToken()
        {
            return GetParameter("token");
        }

        protected bool TokenValidated(string token)
        {
            return DatabaseInteractor.Instance().ValidateToken(token);
        }

        public T? GetData<T>() where T : class
        {
            using var reader = new StreamReader(context.Request.InputStream);
            string json = reader.ReadToEnd();
            try
            {
                var request = JsonSerializer.Deserialize<Request>(json);
                if (request != null)
                {
                    var data = JsonSerializer.Deserialize<T>((JsonElement)request.Data);
                    return data;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
