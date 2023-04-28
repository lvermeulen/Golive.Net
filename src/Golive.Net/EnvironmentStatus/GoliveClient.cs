using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Golive.Net.Models;

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetEnvironmentStatusUrl() => GetBaseUrl().AppendPathSegment("/status");

    private IFlurlRequest GetEnvironmentStatusesUrl() => GetBaseUrl().AppendPathSegment("/statuses");

    public async Task<IEnumerable<EnvironmentStatus>> GetEnvironmentStatusesAsync() => await GetEnvironmentStatusesUrl()
        .GetJsonAsync<IEnumerable<EnvironmentStatus>>()
        .ConfigureAwait(false);

    public async Task<EnvironmentStatus> GetEnvironmentStatusAsync(string application, string category, int? environmentId = null, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        return await GetEnvironmentStatusUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .GetJsonAsync<EnvironmentStatus>()
            .ConfigureAwait(false);
    }

    public async Task<EnvironmentStatus> GetEnvironmentStatusAsync(int id) => await GetEnvironmentStatusUrl()
        .AppendPathSegment(id)
        .GetJsonAsync<EnvironmentStatus>()
        .ConfigureAwait(false);

    public async Task<EnvironmentStatus?> UpdateEnvironmentStatusAsync(string application, string category, int environmentId, object environmentStatus, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        var response = await GetEnvironmentStatusUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .PutJsonAsync(environmentStatus)
            .ConfigureAwait(false);

        return await HandleResponseAsync<EnvironmentStatus>(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<EnvironmentStatus?> DeleteEnvironmentStatusAsync(int id)
    {
        var response = await GetEnvironmentStatusUrl()
            .AppendPathSegment(id)
            .DeleteAsync()
            .ConfigureAwait(false);

        return await HandleResponseAsync<EnvironmentStatus>(response.ResponseMessage).ConfigureAwait(false);
    }
}