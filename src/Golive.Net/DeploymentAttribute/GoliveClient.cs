using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Golive.Net.Models;

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetDeploymentAttributeUrl() => GetBaseUrl().AppendPathSegment("/deployment/attribute");

    private IFlurlRequest GetDeploymentAttributesUrl() => GetBaseUrl().AppendPathSegment("/deployment/attributes");

    public async Task<IEnumerable<DeploymentAttribute>> GetDeploymentAttributesAsync() => await GetDeploymentAttributesUrl()
        .GetJsonAsync<IEnumerable<DeploymentAttribute>>()
        .ConfigureAwait(false);

    public async Task<DeploymentAttribute> GetDeploymentAttributeAsync(string application, string category, int? environmentId = null, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        return await GetDeploymentAttributeUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .GetJsonAsync<DeploymentAttribute>()
            .ConfigureAwait(false);
    }

    public async Task<DeploymentAttribute> GetDeploymentAttributeAsync(int id) => await GetDeploymentAttributeUrl()
        .AppendPathSegment(id)
        .GetJsonAsync<DeploymentAttribute>()
        .ConfigureAwait(false);

    public async Task<DeploymentAttribute?> UpdateDeploymentAttributeAsync(string application, string category, int environmentId, object deploymentAttribute, string? dateTime = null, string? time = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(application)] = application,
            [nameof(category)] = category,
            [nameof(environmentId)] = environmentId,
            [nameof(dateTime)] = dateTime,
            [nameof(time)] = time,
        };

        var response = await GetDeploymentAttributeUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .PutJsonAsync(deploymentAttribute)
            .ConfigureAwait(false);

        return await HandleResponseAsync<DeploymentAttribute>(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<DeploymentAttribute?> DeleteDeploymentAttributeAsync(int id)
    {
        var response = await GetDeploymentAttributeUrl()
            .AppendPathSegment(id)
            .DeleteAsync()
            .ConfigureAwait(false);

        return await HandleResponseAsync<DeploymentAttribute>(response.ResponseMessage).ConfigureAwait(false);
    }
}