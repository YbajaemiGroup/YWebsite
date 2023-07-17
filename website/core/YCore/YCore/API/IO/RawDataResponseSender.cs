using System.Net;

namespace YCore.API.IO
{
    public class RawDataResponseSender : IResponseSender
    {
        private readonly Stream data;

        public RawDataResponseSender(Stream data)
        {
            this.data = data;
        }

        public void Send(Stream outputStream)
        {
            if (!outputStream.CanWrite)
            {
                throw new AccessViolationException();
            }
            data.CopyTo(outputStream);
            outputStream.Flush();
            data.Close();
        }
    }
}
