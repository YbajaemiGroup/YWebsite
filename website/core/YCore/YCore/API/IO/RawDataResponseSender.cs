namespace YCore.API.IO
{
    public class RawDataResponseSender : IResponseSender
    {
        private readonly byte[] data;

        public RawDataResponseSender(byte[] data)
        {
            this.data = data;
        }

        public void Send(Stream outputStream)
        {
            if (!outputStream.CanWrite)
            {
                throw new AccessViolationException();
            }
            using var streamWriter = new BinaryWriter(outputStream);
            streamWriter.Write(data);
            streamWriter.Flush();
        }
    }
}
