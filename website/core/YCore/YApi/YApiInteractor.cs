using System.Diagnostics;
using YApiModel.Models;

namespace YApi
{
    public class YApiInteractor : IApiInteractor
    {
        private readonly YClient _client;

        public YApiInteractor(IConfigInteractor configInteractor)
        {
            _client = new YClient(configInteractor.GetToken());
        }

        public List<Player> GetAllPlayers()
        {
            return _client.PlayersGetAsync().Result;
        }

        public Task<List<Player>> GetAllPlayersAsync()
        {
            return _client.PlayersGetAsync();
        }

        public Task<List<Player>> UpdatePlayersAsync(List<Player> players)
        {
            return _client.PlayersAddOrUpdateAsync(players);
        }

        public async Task DeletePlayer(int playerId)
        {
            await _client.PlayerDelete(playerId);
        }

        public Task<List<Image>> GetImagesListAsync()
        {
            return _client.GetImagesList();
        }

        public void DownloadImage(string imageName, string imagesDirectory)
        {
            using var dbImage = _client.GetImage(imageName, YApiModel.ImageType.Players).Result;
            if (dbImage == null)
            {
                throw new NullReferenceException("dbImage was null");
            }
            string imagePath = $"{imagesDirectory}\\{imageName}";
            if (!File.Exists(imagePath))
            {
                using var localImage = File.OpenWrite(imagePath);
                dbImage.CopyTo(localImage);
                localImage.Flush();
                dbImage.Flush();
            }
        }

        public async Task DownloadImageAsync(string imageName, string imagesDirectory)
        {
            using var dbImage = await _client.GetImage(imageName, YApiModel.ImageType.Players);
            if (dbImage == null)
            {
                throw new NullReferenceException("No image found in database.");
            }
            string imagePath = $"{imagesDirectory}\\{imageName}";
            if (!File.Exists(imagePath))
            {
                using var localImage = File.OpenWrite(imagePath);
                await dbImage.CopyToAsync(localImage);
                await localImage.FlushAsync().ConfigureAwait(false);
                await dbImage.FlushAsync().ConfigureAwait(false);
            }
        }

        public async Task DownloadImageAsync(string imageName, Stream stream)
        {
            if (!stream.CanWrite)
            {
                throw new ArgumentException("Can't write to stream.", nameof(stream));
            }
            using var dbImage = await _client.GetImage(imageName, YApiModel.ImageType.Players);
            if (dbImage == null)
            {
                throw new NullReferenceException("No image found in database.");
            }
            await dbImage.CopyToAsync(stream);
        }

        public List<string> DownloadAllImages(string imageDirectory)
        {
            var images = _client.GetImagesList().Result;
            var imagesNames = new List<string>();
            foreach (var image in images.Select(i => i.ImageName))
            {
                if (image != null)
                {
                    try
                    {
                        DownloadImage(image, imageDirectory);
                    }
                    catch (NullReferenceException)
                    {
                        continue;
                    }
                    imagesNames.Add(image);
                }
            }
            return imagesNames;
        }

        public async IAsyncEnumerable<string> DownloadAllImagesAsync(string imageDirectory)
        {
            var images = await _client.GetImagesList();
            foreach (var image in images.Select(i => i.ImageName))
            {
                if (image != null)
                {
                    try
                    {
                        await DownloadImageAsync(image, imageDirectory);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    yield return image;
                }
            }
        }

        public Image LoadImageToServer(string imageName, Stream stream)
        {
            ArgumentException.ThrowIfNullOrEmpty(imageName);
            if (!stream.CanRead)
            {
                throw new ArgumentException("Can not read stream.", nameof(stream));
            }
            return _client.LoadImageAsync(imageName, stream).GetAwaiter().GetResult();
        }

        public Task<Image> LoadImageToServerAsync(string imageName, Stream stream)
        {
            ArgumentException.ThrowIfNullOrEmpty(imageName);
            if (!stream.CanRead)
            {
                throw new ArgumentException("Can not read stream.", nameof(stream));
            }
            return _client.LoadImageAsync(imageName, stream);
        }

        public Task LoadTokenToServerAsync(string tokenSource)
        {
            ArgumentException.ThrowIfNullOrEmpty(tokenSource);
            return _client.CreateTokenAsync(tokenSource);
        }

        public Task DeleteTokenFromServerAsync(string tokenSource)
        {
            ArgumentException.ThrowIfNullOrEmpty(tokenSource);
            return _client.DeleteTokenAsync(tokenSource);
        }

        public Task<List<Link>> GetAllLinksAsync()
        {
            return _client.GetLinksAsync();
        }

        public Task<List<Link>> PostLinksAsync(List<Link> links)
        {
            return _client.AddLinksAsync(links);
        }

        public Task DeleteLinkAsync(int linkId)
        {
            return _client.DeleteLinksAsync(linkId);   
        }

        public Task<List<Round>> GetBracketAsync()
        {
            return _client.GetBracketAsync();
        }

        public Task PostBracketAsync(List<Round> bracket)
        {
            return _client.SetBracketAsync(bracket);
        }
    }
}
