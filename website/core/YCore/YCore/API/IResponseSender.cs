namespace YCore.API
{
    public interface IResponseSender
    {
        public void Send(Stream outputStream);
    }
}
