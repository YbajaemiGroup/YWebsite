using System.Net;
using System.Text.Json;
using YApiModel;

namespace YCore.API.IO
{
    public class JsonResponseSender : IResponseSender
    {
        private readonly Response _response;

        public JsonResponseSender(Response response)
        {
            _response = response;
        }

        public void Send(HttpListenerResponse response)
        {
            response.Headers.Add("Content-Type", "application/json");
            if (!response.OutputStream.CanWrite)
            {
                throw new AccessViolationException();
            }
            using var streamWriter = new StreamWriter(response.OutputStream);
            streamWriter.Write(JsonSerializer.Serialize(_response));
            response.OutputStream.Flush();
        }
    }
}
