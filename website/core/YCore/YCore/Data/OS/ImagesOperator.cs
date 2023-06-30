using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCore.Data.OS
{
    public class ImagesOperator
    {
        private readonly string imagesLocation;

        public ImagesOperator(string imagesLocation)
        {
            this.imagesLocation = imagesLocation;
        }

        private byte[] ReadAllBytes(Stream reader)
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
        /// <param name="imageName"></param>
        /// <returns>stream that needs to be closed manualy</returns>
        public Stream? GetImage(string imageName)
        {
            try
            {
                string uri = $"{imagesLocation}/{imageName}";
                var file = File.OpenRead(uri);
                return file;
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Log(LogSeverity.Error, nameof(ImagesOperator), "Error occured on reading image.", e);
            }
            catch (DirectoryNotFoundException e)
            {
                Logger.Log(LogSeverity.Error, nameof(ImagesOperator), "Error occured on reading image.", e);
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Info, nameof(ImagesOperator), "Error occured on reading image.", e);
            }
            return null;
        }

        public bool SaveImage(string imageName, Stream stream, long offset = 0)
        {
            if (!SecurityUtilities.ValidateImageName(imageName))
            {
                return false;
            }
            try
            {
                string uri = $"{imagesLocation}/{imageName}";
                using var fileStream = File.Create(uri);
                stream.Position = offset;
                stream.CopyTo(fileStream);
                fileStream.Flush();
                stream.Close();
                return true;
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Log(LogSeverity.Error, nameof(ImagesOperator), "Error occured on loading image.", e);
            }
            catch (DirectoryNotFoundException e)
            {
                Logger.Log(LogSeverity.Error, nameof(ImagesOperator), "Error occured on loading image.", e);
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Info, nameof(ImagesOperator), "Error occured on loading image.", e);
            }
            return false;
        }

        public bool DeleteImage(string imageName)
        {
            string uri = $"{imagesLocation}/{imageName}";
            try
            {
                File.Delete(uri);
                return true;
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Info, nameof(ImagesOperator), "Error occured on deleting image.", e);
                return false;
            }
        }
    }
}
