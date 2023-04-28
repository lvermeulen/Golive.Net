using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using Golive.Net.Models;

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetEnvironmentUrl() => GetBaseUrl().AppendPathSegment("/environment");

    public async Task<Environment?> CreateEnvironmentAsync(object? environment)
    {
        var response = await GetEnvironmentUrl()
            .PostAsync(new StringContent(_serializer.Serialize(environment)))
            .ConfigureAwait(false);

        return await HandleResponseAsync<Environment>(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<Environment> GetEnvironmentsAsync(string application, string category)
    {
        var queryParams = new Dictionary<string, object>
        {
            [nameof(application)] = application,
            [nameof(category)] = category
        };

        return await GetEnvironmentUrl()
            .SetQueryParams(queryParams)
            .GetJsonAsync<Environment>()
            .ConfigureAwait(false);
    }

    public async Task<Environment> GetEnvironmentAsync(int id) => await GetEnvironmentUrl()
        .AppendPathSegment(id)
        .GetJsonAsync<Environment>()
        .ConfigureAwait(false);

    public async Task<Environment?> UpdateEnvironmentAsync(int id, Environment environment)
    {
        var response = await GetEnvironmentUrl()
            .AppendPathSegment(id)
            .PutJsonAsync(environment)
            .ConfigureAwait(false);

        return await HandleResponseAsync<Environment>(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<Environment?> DeleteEnvironmentAsync(int id)
    {
        var response = await GetEnvironmentUrl()
            .AppendPathSegment(id)
            .DeleteAsync()
            .ConfigureAwait(false);

        return await HandleResponseAsync<Environment>(response.ResponseMessage).ConfigureAwait(false);
    }
}