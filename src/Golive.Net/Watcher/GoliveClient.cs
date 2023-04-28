using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Golive.Net.Models;

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetWatcherStatisticsUrl() => GetBaseUrl().AppendPathSegment("/watcher/user");

    private IFlurlRequest GetWatcherByEnvironmentUrl() => GetBaseUrl().AppendPathSegment("/watcher/statistics");

    public async Task<WatcherStatistics> GetWatcherStatisticsAsync()
    {
        return await GetWatcherStatisticsUrl()
            .GetJsonAsync<WatcherStatistics>()
            .ConfigureAwait(false);
    }

    public async Task<Watcher> GetWatcherByEnvironmentAsync(int environmentId, string? userName = null, string? userKey = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(environmentId)] = environmentId,
            [nameof(userName)] = userName,
            [nameof(userKey)] = userKey
        };

        return await GetWatcherByEnvironmentUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .GetJsonAsync<Watcher>()
            .ConfigureAwait(false);
    }
}