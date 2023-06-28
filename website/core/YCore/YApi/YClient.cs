using System.Text.Json;
using System.Xml.Serialization;
using YApiModel;
using YApiModel.Models;

namespace YApi;

public class YClient
{
    private const string URL = "";

    public string Token { get; set; }
    public bool TokenLoaded { get => Token != null; }
    private RequestSender requestSender;

    public YClient(string token)
    {
        Token = token;
        requestSender = new(URL);
    }

    /// <summary>
    /// Throws exception if server returned error.
    /// </summary>
    /// <param name="response"></param>
    /// <exception cref="YException"></exception>
    private void CheckException(Response response)
    {
        if (response.Exception != null)
        {
            throw response.Exception;
        }
    }

    private T GetResponseData<T>(Response response)
    {
        if (response.ResponseData == null)
        {
            throw new NullReferenceException("ResponseData was null.");
        }
        var data = JsonSerializer.Deserialize<T>((JsonDocument)response.ResponseData);
        if (data == null)
        {
            throw new NullReferenceException("Deserialized object was null.");
        }
        return data;
    }

    public async Task<List<Round>> GetBracketAsync()
    {
        var response = await requestSender.SendRequestAsync("bracket.updates.get");
        CheckException(response);
        return GetResponseData<List<Round>>(response);
    }

    public async Task SetBracketAsync(List<Round> bracket)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("bracket.updates.set", parameters, new(bracket));
        CheckException(response);
    }

    public async Task GroupFillAsync(List<GroupFillData> groups)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("group.fill", parameters, new(groups));
        CheckException(response);
    }

    public async Task GroupGetGames()
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("group.games.get", parameters);
        CheckException(response);
    }

    public async Task<List<Link>> GetLinksAsync(int playerId)
    {
        var parameters = new HttpParameters();
        parameters.Add("player_id", playerId);
        var response = await requestSender.SendRequestAsync("links.get", parameters);
        CheckException(response);
        return GetResponseData<List<Link>>(response);
    }

    public async Task<List<Link>> GetLinksAsync()
    {
        var response = await requestSender.SendRequestAsync("links.get");
        CheckException(response);
        return GetResponseData<List<Link>>(response);
    }

    public async Task<List<Link>> AddLinksAsync(List<Link> links)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("link.add", parameters, new(links));
        CheckException(response);
        return GetResponseData<List<Link>>(response);
    }

    public async Task DeleteLinksAsync(int linkId)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("link_id", linkId);
        var response = await requestSender.SendRequestAsync("links.delete", parameters);
        CheckException(response);
    }

    /// <summary>
    /// Add or update players in database. Adds player if id is null. Overwise, update existing one.
    /// </summary>
    /// <param name="players">List of players need to add or update.</param>
    /// <returns>List updated of players from database.</returns>
    public async Task<List<Player>> PlayersAddOrUpdateAsync(List<Player> players, out List<Player>? notProcessedPlayers)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("players.add", parameters);
        CheckException(response);
        return GetResponseData<List<Player>>(response);
    }

    public async Task<List<Player>> PlayersGetAsync()
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("players.get", parameters);
        CheckException(response);
        return GetResponseData<List<Player>>(response);
    }

    public async Task<Player> PlayerDelete(int playerId)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("player_id", playerId);
        var response = await requestSender.SendRequestAsync("players.delete", parameters);
        CheckException(response);
        return GetResponseData<Player>(response);
    }

    public async Task<byte[]> GetImage(string imageName, ImageType imageType)
    {
        string imageTypeName = imageType switch
        {
            ImageType.Resources => "resources",
            ImageType.Players => "players",
            _ => throw new ArgumentOutOfRangeException(nameof(imageType))
        };
        var parameters = new HttpParameters();
        parameters.Add("image_name", imageName);
        parameters.Add("image_type", imageTypeName);
        return await requestSender.DownloadImageAsync("images.get", parameters);
    }

    public async Task<Image> LoadImageAsync(string imageName, byte[] imageBytes)
    {
        throw new NotImplementedException();
    }
}