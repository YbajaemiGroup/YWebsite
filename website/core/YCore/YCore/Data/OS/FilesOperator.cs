namespace YCore.Data.OS
{
    public class FilesOperator
    {
        private readonly string filesLocation;

        public FilesOperator(string filesLocation)
        {
            this.filesLocation = filesLocation;
        }

        public static string? GetFileExtention(string fileName) => fileName.Split('.').LastOrDefault();

        private static byte[] ReadAllBytes(Stream reader)
        {
            const int bufferSize = 4096;
            using var ms = new MemoryStream();
            byte[] buffer = new byte[bufferSize];
            int count;
            while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
            {
                ms.Write(buffer, 0, count);
            }
            return ms.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>stream that needs to be closed manualy</returns>
        public Stream? GetFile(string fileName)
        {
            try
            {
                string uri = $"{filesLocation}/{fileName}";
                var file = File.OpenRead(uri);
                return file;
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Log(LogSeverity.Error, nameof(FilesOperator), "Error occured on reading file.", e);
            }
            catch (DirectoryNotFoundException e)
            {
                Logger.Log(LogSeverity.Error, nameof(FilesOperator), "Error occured on reading file.", e);
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Info, nameof(FilesOperator), "Error occured on reading file.", e);
            }
            return null;
        }

        public bool SaveFile(string fileName, Stream stream)
        {
            if (!SecurityUtilities.ValidateFileName(fileName))
            {
                return false;
            }
            try
            {
                string uri = $"{filesLocation}/{fileName}";
                using var fileStream = File.Create(uri);
                stream.CopyTo(fileStream);
                fileStream.Flush();
                stream.Flush();
                stream.Close();
                return true;
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Log(LogSeverity.Error, nameof(FilesOperator), "Error occured on loading file.", e);
            }
            catch (DirectoryNotFoundException e)
            {
                Logger.Log(LogSeverity.Error, nameof(FilesOperator), "Error occured on loading file.", e);
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Info, nameof(FilesOperator), "Error occured on loading file.", e);
            }
            return false;
        }

        public bool DeleteFile(string fileName)
        {
            string uri = $"{filesLocation}/{fileName}";
            try
            {
                File.Delete(uri);
                return true;
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Info, nameof(FilesOperator), "Error occured on deleting file.", e);
                return false;
            }
        }
    }
}
