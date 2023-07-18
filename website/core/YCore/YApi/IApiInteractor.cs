using YApiModel.Models;

namespace YApi
{
    public interface IApiInteractor
    {
        Task DeletePlayer(int playerId);
        Task DeleteTokenFromServerAsync(string tokenSource);
        IAsyncEnumerable<string> DownloadAllImagesAsync(string imageDirectory);
        void DownloadImage(string imageName, string imagesDirectory);
        Task DownloadImageAsync(string imageName, Stream stream);
        Task DownloadImageAsync(string imageName, string imagesDirectory);
        List<string> DownloadAllImages(string imageDirectory);
        List<Player> GetAllPlayers();
        Task<List<Player>> GetAllPlayersAsync();
        Image LoadImageToServer(string imageName, Stream stream);
        Task<Image> LoadImageToServerAsync(string imageName, Stream stream);
        Task LoadTokenToServerAsync(string tokenSource);
        Task<List<Player>> UpdatePlayersAsync(List<Player> players);
        Task DeleteLinkAsync(int value);
        Task<List<Link>> PostLinksAsync(List<Link> links);
        Task<List<Link>> GetAllLinksAsync();
        Task<List<Round>> GetBracketAsync();
        Task PostBracketAsync(List<Round> bracket);
    }
}