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
            using var writer = new BinaryWriter(outputStream);
            byte[] bytes = new byte[data.Length];
            data.Read(bytes, 0, bytes.Length);
            writer.Write(bytes);
            outputStream.Flush();
        }
    }
}
