using System.Net.Http.Json;
using System.Text.Json;
using YApiModel;

namespace YApi;

internal class RequestSender
{
    private readonly string _url;
    private readonly HttpClient httpClient;

    public RequestSender(string url)
    {
        _url = url.EndsWith('/') ? url : url + "/";
        httpClient = new HttpClient();
    }

    public string GetUrlWithParameters(string method, HttpParameters httpParameters) => $"{_url}{method}?{httpParameters}";

    /// <summary>
    /// Downloading image from server.
    /// </summary>
    /// <param name="method">Downloading api method</param>
    /// <param name="parameters">Request parameters</param>
    /// <returns>Selected image byte stream (need to be closed from calling method)</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<Stream?> DownloadImageAsync(string method, HttpParameters parameters)
    {
        using var message = new HttpRequestMessage();
        message.RequestUri = new(parameters == null ? _url : GetUrlWithParameters(method, parameters));
        var requestSending = httpClient.SendAsync(message);
        var responseMessage = await requestSending;
        if (!requestSending.IsCompletedSuccessfully)
        {
            throw requestSending.Exception ?? new Exception("Task httpClient.SendAsync returned unsuccessfully with no exception.");
        }
        Response response = null!;
        try
        {
            var r = await responseMessage.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<Response>(r) ?? throw new ArgumentNullException();
            throw new Exception(response.Exception?.ToString());
        }
        catch (Exception e) when (e is ArgumentNullException or JsonException or NotSupportedException)
        {
            var imageBytes = await responseMessage.Content.ReadAsStreamAsync();
            return imageBytes;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Sends request to server. In message body writes json serialized <paramref name="request"/>
    /// </summary>
    /// <param name="method">Api method</param>
    /// <param name="parameters">Request parameters</param>
    /// <param name="request">Request data</param>
    /// <returns>Server response object</returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<Response> SendRequestAsync(string method, HttpParameters? parameters = null, Request? request = null)
    {
        using var message = new HttpRequestMessage();
        message.RequestUri = new(parameters == null ? _url + method : GetUrlWithParameters(method, parameters));
        if (request != null)
        {
            message.Content = JsonContent.Create(request);
        }
        var requestSending = httpClient.SendAsync(message);
        var responseMessage = await requestSending;
        if (!requestSending.IsCompletedSuccessfully)
        {
            throw requestSending.Exception ?? new Exception("Task httpClient.SendAsync returned unsuccessfully with no exception.");
        }
        var responseString = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<Response>(responseString);
        if (response == null)
        {
            throw new NullReferenceException("Server somehow returned null.");
        }
        return response;
    }

    /// <summary>
    /// Sends custom HttpContent to server.
    /// </summary>
    /// <param name="method">Api method</param>
    /// <param name="parameters">Request parameters</param>
    /// <param name="content">Custom content</param>
    /// <returns>Server response object</returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<Response> SendRequestAsync(string method, HttpParameters? parameters, HttpContent content)
    {
        using var message = new HttpRequestMessage();
        message.RequestUri = new(parameters == null ? _url : GetUrlWithParameters(method, parameters));
        message.Content = content;
        var requestSending = httpClient.SendAsync(message);
        var responseMessage = await requestSending;
        if (!requestSending.IsCompletedSuccessfully)
        {
            throw requestSending.Exception ?? new Exception("Task httpClient.SendAsync returned unsuccessfully with no exception.");
        }
        var responseString = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<Response>(responseString);
        if (response == null)
        {
            throw new NullReferenceException("Server somehow returned null.");
        }
        return response;
    }
}
