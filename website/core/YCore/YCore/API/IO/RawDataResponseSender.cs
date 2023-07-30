using System.Net;

namespace YCore.API.IO
{
    public class RawDataResponseSender : IResponseSender
    {
        private readonly Stream _data;
        private readonly string? _fileExtention;

        public RawDataResponseSender(Stream fileData)
        {
            _data = fileData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileExtention">File name extention or file type</param>
        /// <param name="fileData"></param>
        public RawDataResponseSender(string? fileExtention, Stream fileData)
        {
            _fileExtention = fileExtention;
            _data = fileData;
        }

        private string? GetMimeType() => _fileExtention switch
        {
            "jpg" => "image/jpeg",
            "png" => "image/png",
            "ico" => "image/x-icon",
            "txt" => "text/plain",
            "csv" => "text/csv",
            "html" => "text/html",
            "js" => "text/javascript",
            "css" => "text/css",
            "ttf" => "font/ttf",
            "woff2" => "font/woff",
            _ => null
        };

        public void Send(HttpListenerResponse response)
        {
            var mimeType = GetMimeType();
            if (mimeType != null)
            {
                response.Headers.Add("Content-Type", mimeType);
            }
            if (!response.OutputStream.CanWrite)
            {
                throw new AccessViolationException();
            }
            _data.CopyTo(response.OutputStream);
            response.OutputStream.Flush();
            _data.Close();
        }
    }
}
