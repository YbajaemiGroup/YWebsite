using System.Text.Json;
using YApiModel;

namespace YCore.API.IO
{
    public class JsonResponseSender : IResponseSender
    {
        private readonly Response response;

        public JsonResponseSender(Response response)
        {
            this.response = response;
        }

        public void Send(Stream outputStream)
        {
            if (!outputStream.CanWrite)
            {
                throw new AccessViolationException();
            }
            using var streamWriter = new StreamWriter(outputStream);
            streamWriter.Write(JsonSerializer.Serialize(response));
            outputStream.Flush();
        }
    }
}
