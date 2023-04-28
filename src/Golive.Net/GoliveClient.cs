using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Golive.Net.Converters;
using Golive.Net.Extensions;
using Golive.Net.Models;
using Environment = System.Environment;

namespace Golive.Net;

public partial class GoliveClient
{
    private ISerializer _serializer;

    private readonly Url _url;
    private readonly string _apiKey;
    private readonly bool _useBearerToken;

    public GoliveClient(string url, string apiKey, bool useBearerToken = false)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new ErrorsConverter(), new UnixToNullableDateTimeConverter() }
        };
        _serializer = new DefaultJsonSerializer(jsonSerializerOptions);

        _url = url;
        _apiKey = apiKey;
        _useBearerToken = useBearerToken;
    }

    private async Task<TResult?> ReadResponseContentAsync<TResult>(HttpResponseMessage responseMessage)
    {
        var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        var result = _serializer.Deserialize<TResult>(content);
        return result!;
    }

    private async Task<bool> ReadResponseContentAsync(HttpResponseMessage responseMessage)
    {
        var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        return content.Length == 0;
    }

    private async Task<TResult?> ReadResponseContentFirstNodeAsync<TResult>(HttpResponseMessage responseMessage)
        where TResult : class
    {
        var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        var node = content.GetJsonPropertyByIndex(0);

        return node is not null
            ? _serializer.Deserialize<TResult>(node.Value.Value.ToString())
            : null;
    }

    private async Task HandleErrorsAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await ReadResponseContentAsync<Error>(response).ConfigureAwait(false);
            if (error is null)
            {
                return;
            }

            var errors = !string.IsNullOrEmpty(error.Errors?.Code)
                ? string.Join(Environment.NewLine, $"{error.Errors.Code} - {error.Errors.Message}")
                : string.Join(Environment.NewLine, error.Warnings ?? Enumerable.Empty<string>());
            var errorMessage = (error.ErrorMessages ?? Enumerable.Empty<string>()).Any()
                ? string.Join(Environment.NewLine, error.ErrorMessages ?? Enumerable.Empty<string>())
                : errors;
            throw new InvalidOperationException($"Http request failed ({(int)response.StatusCode} - {response.StatusCode}):\n{errorMessage}");
        }
    }

    private async Task<TResult?> HandleResponseAsync<TResult>(HttpResponseMessage responseMessage)
    {
        await HandleErrorsAsync(responseMessage).ConfigureAwait(false);
        return await ReadResponseContentAsync<TResult>(responseMessage).ConfigureAwait(false);
    }

    private async Task<bool> HandleResponseAsync(HttpResponseMessage responseMessage)
    {
        await HandleErrorsAsync(responseMessage).ConfigureAwait(false);
        return await ReadResponseContentAsync(responseMessage).ConfigureAwait(false);
    }

    private async Task<TResult?> HandleResponseFirstNodeAsync<TResult>(HttpResponseMessage responseMessage)
        where TResult : class
    {
        await HandleErrorsAsync(responseMessage).ConfigureAwait(false);
        return await ReadResponseContentFirstNodeAsync<TResult>(responseMessage).ConfigureAwait(false);
    }

    public void SetSerializer(ISerializer serializer)
    {
        _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
    }

    private IFlurlRequest GetBaseUrl() => new Url(_url)
        .ConfigureRequest(settings => settings.JsonSerializer = _serializer)
        .AllowAnyHttpStatus()
        .WithHeader("Content-Type", "application/json")
        .WithAuthentication(_apiKey, _useBearerToken);
}
