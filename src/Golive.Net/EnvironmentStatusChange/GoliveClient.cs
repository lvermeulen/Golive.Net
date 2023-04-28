using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Golive.Net.Models;

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetEnvironmentStatusChangeUrl() => GetBaseUrl().AppendPathSegment("/status-change");

    private IFlurlRequest GetEnvironmentStatusChangesUrl() => GetBaseUrl().AppendPathSegment("/status-changes");

    public async Task<IEnumerable<EnvironmentStatusChange>> GetEnvironmentStatusChangesAsync()
    {
        return await GetEnvironmentStatusChangesUrl()
            .GetJsonAsync<IEnumerable<EnvironmentStatusChange>>()
            .ConfigureAwait(false);
    }

    public async Task<EnvironmentStatusChange> GetEnvironmentStatusChangeAsync(string application, string category, int? environmentId = null, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        return await GetEnvironmentStatusChangeUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .GetJsonAsync<EnvironmentStatusChange>()
            .ConfigureAwait(false);
    }

    public async Task<EnvironmentStatusChange> GetEnvironmentStatusChangeAsync(int id) => await GetEnvironmentStatusChangeUrl()
        .AppendPathSegment(id)
        .GetJsonAsync<EnvironmentStatusChange>()
        .ConfigureAwait(false);

    public async Task<EnvironmentStatusChange?> UpdateEnvironmentStatusChangeAsync(string application, string category, int environmentId, object environmentStatus, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        var response = await GetEnvironmentStatusChangeUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .PutJsonAsync(environmentStatus)
            .ConfigureAwait(false);

        return await HandleResponseAsync<EnvironmentStatusChange>(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<EnvironmentStatusChange?> DeleteEnvironmentStatusChangeAsync(int id)
    {
        var response = await GetEnvironmentStatusChangeUrl()
            .AppendPathSegment(id)
            .DeleteAsync()
            .ConfigureAwait(false);

        return await HandleResponseAsync<EnvironmentStatusChange>(response.ResponseMessage).ConfigureAwait(false);
    }
}