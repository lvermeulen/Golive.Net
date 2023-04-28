using System.Linq;
using System.Text.Json;
using Flurl.Http;

namespace Golive.Net.Extensions;

public static class FlurlRequestExtensions
{
    internal static IFlurlRequest WithAuthentication(this IFlurlRequest request, string apiKey, bool useBearerToken = false) => useBearerToken
        ? request.WithOAuthBearerToken(apiKey)
        : request.WithHeader("api-key", apiKey);

    internal static JsonProperty? Get(this JsonElement element, int index)
    {
        switch (element)
        {
            case { ValueKind: JsonValueKind.Object }:
            {
                var props = element.EnumerateObject().ToList();
                return props[index];
            }
            default:
                return null;
        }
    }

    internal static JsonProperty? GetJsonPropertyByIndex(this string json, int index)
    {
        var doc = JsonSerializer.Deserialize<JsonElement>(json);
        var node = doc.Get(index);

        return node;
    }
}
