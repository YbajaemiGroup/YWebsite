using System.Net;

namespace YCore.API
{
    public interface IResponseSender
    {
        void Send(HttpListenerResponse response);
    }
}
