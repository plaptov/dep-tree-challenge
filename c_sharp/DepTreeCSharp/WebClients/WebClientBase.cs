using System.Text.Json;
using Microsoft.Extensions.Options;
using DepTreeCSharp.Exceptions;
using DepTreeCSharp.Models;

namespace DepTreeCSharp.WebClients;

public abstract class WebClientBase
{
    private const string _baseUrl = "https://libraries.io/api/";
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = new SnakeCaseJsonNamingPolicy(),
    };

    private readonly HttpClient _client;

    public WebClientBase(IOptions<ApiSettings> settings)
    {
        ApiKey = settings.Value.ApiKey;
        _client = new HttpClient
        {
            BaseAddress = new Uri(_baseUrl),
        };
    }

    protected string ApiKey { get; }

    protected async Task<T> Get<T>(string url, CancellationToken cancellationToken = default)
    {
        var requestUri = $"{url}?api_key={ApiKey}";
        var req = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await _client.SendAsync(req, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            throw new TooManyRequestsException();
        return await response.Content.ReadFromJsonAsync<T>(_serializerOptions, cancellationToken);
    }
}