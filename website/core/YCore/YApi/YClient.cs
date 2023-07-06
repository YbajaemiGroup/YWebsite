using System.Text.Json;
using System.Xml.Serialization;
using YApiModel;
using YApiModel.Models;

namespace YApi;

public class YClient
{
    private const string URL = "http://localhost:12345/api/";

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
    private void CheckException(Response response)
    {
        if (response.Exception != null)
        {
            throw new Exception(response.Exception.ToString());
        }
    }

    private T GetResponseData<T>(Response response)
    {
        if (response.ResponseData == null)
        {
            throw new NullReferenceException("ResponseData was null.");
        }
        if (response.ResponseData is JsonDocument document)
        {
            var data = JsonSerializer.Deserialize<T>(document);
            if (data == null)
            {
                throw new NullReferenceException("Deserialized object was null.");
            }
            return data;
        }
        else if (response.ResponseData is JsonElement element)
        {
            var data = JsonSerializer.Deserialize<T>(element);
            if (data == null)
            {
                throw new NullReferenceException("Deserialized object was null.");
            }
            return data;
        }
        throw new ArgumentException("Response data wasn't JsonDocument or JsonElement.", nameof(response));
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
        var response = await requestSender.SendRequestAsync("bracket.updates.set", parameters, new Request(bracket));
        CheckException(response);
    }

    public async Task GroupFillAsync(List<GroupFillData> groups)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("group.fill", parameters, new Request(groups));
        CheckException(response);
    }

    public async Task<List<GroupGetData>> GroupGetGames()
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("group.get", parameters);
        CheckException(response);
        return GetResponseData<List<GroupGetData>>(response);
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
        var response = await requestSender.SendRequestAsync("links.add", parameters, new Request(links));
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
    public async Task<List<Player>> PlayersAddOrUpdateAsync(List<Player> players)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("players.add", parameters, new Request(players));
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

    public async Task<Stream> GetImage(string imageName, ImageType imageType)
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
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("image_name", imageName);
        var content = new MultipartFormDataContent();
        var fileStreamContent = new StreamContent(new MemoryStream(imageBytes));
        fileStreamContent.Headers.ContentType = new(imageName.Split('.')[1] switch
        {
            "jpeg" => "image/jpeg",
            "jpg" => "image/jpeg",
            "png" => "image/png",
            _ => throw new ArgumentException(nameof(imageName), "Image should be .png .jpg of .jpeg")
        });
        content.Add(fileStreamContent, "file", imageName);
        var response = await requestSender.SendRequestAsync("images.load", parameters, imageBytes);
        CheckException(response);
        return GetResponseData<Image>(response);
    }

    public async Task CreateToken(string tokenSource)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("token_source", tokenSource);
        var response = await requestSender.SendRequestAsync("token.create", parameters);
        CheckException(response);
    }

    public async Task DeleteToken(string token)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("d_token", token);
        var response = await requestSender.SendRequestAsync("token.delete", parameters);
        CheckException(response);
    }
}