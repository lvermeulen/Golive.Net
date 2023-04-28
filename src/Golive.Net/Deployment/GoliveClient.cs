using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Golive.Net.Models;

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetDeploymentUrl() => GetBaseUrl().AppendPathSegment("/deployment");

    private IFlurlRequest GetDeploymentsUrl() => GetBaseUrl().AppendPathSegment("/deployments");

    public async Task<IEnumerable<Deployment>> GetDeploymentsAsync(string application, string category, int? environmentId = null, string? start = null, string? end = null, int? maxResults = null, bool? expand = false)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(start)] = start,
            [nameof(end)] = end,
            [nameof(maxResults)] = maxResults,
            [nameof(expand)] = expand
        };

        return await GetDeploymentsUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .GetJsonAsync<IEnumerable<Deployment>>()
            .ConfigureAwait(false);
    }

    public async Task<Deployment> GetDeploymentAsync(string application, string category, int? environmentId = null, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        return await GetDeploymentUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .GetJsonAsync<Deployment>()
            .ConfigureAwait(false);
    }

    public async Task<Deployment> GetDeploymentAsync(int id) => await GetDeploymentUrl()
        .AppendPathSegment(id)
        .GetJsonAsync<Deployment>()
        .ConfigureAwait(false);

    public async Task<Deployment?> UpdateDeploymentAsync(string application, string category, int environmentId, object deployment, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        var response = await GetDeploymentUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .PutJsonAsync(deployment)
            .ConfigureAwait(false);

        return await HandleResponseAsync<Deployment>(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<Deployment?> DeleteDeploymentAsync(int id)
    {
        var response = await GetDeploymentUrl()
            .AppendPathSegment(id)
            .DeleteAsync()
            .ConfigureAwait(false);

        return await HandleResponseAsync<Deployment>(response.ResponseMessage).ConfigureAwait(false);
    }
}