using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Golive.Net.Models;

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetEnvironmentAttributeUrl() => GetBaseUrl().AppendPathSegment("/environment/attribute");

    private IFlurlRequest GetEnvironmentAttributesUrl() => GetBaseUrl().AppendPathSegment("/environment/attributes");

    public async Task<IEnumerable<OrderedAttribute>> GetEnvironmentAttributesAsync() => await GetEnvironmentAttributesUrl()
        .GetJsonAsync<IEnumerable<OrderedAttribute>>()
        .ConfigureAwait(false);

    public async Task<OrderedAttribute> GetEnvironmentAttributeAsync(string application, string category, int? environmentId = null, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        return await GetEnvironmentAttributeUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .GetJsonAsync<OrderedAttribute>()
            .ConfigureAwait(false);
    }

    public async Task<OrderedAttribute> GetEnvironmentAttributeAsync(int id) => await GetEnvironmentAttributeUrl()
        .AppendPathSegment(id)
        .GetJsonAsync<OrderedAttribute>()
        .ConfigureAwait(false);

    public async Task<OrderedAttribute?> UpdateEnvironmentAttributeAsync(string application, string category, int environmentId, object orderedAttribute, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        var response = await GetEnvironmentAttributeUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .PutJsonAsync(orderedAttribute)
            .ConfigureAwait(false);

        return await HandleResponseAsync<OrderedAttribute>(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<OrderedAttribute?> DeleteEnvironmentAttributeAsync(int id)
    {
        var response = await GetEnvironmentAttributeUrl()
            .AppendPathSegment(id)
            .DeleteAsync()
            .ConfigureAwait(false);

        return await HandleResponseAsync<OrderedAttribute>(response.ResponseMessage).ConfigureAwait(false);
    }
}