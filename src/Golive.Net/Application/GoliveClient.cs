using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Golive.Net.Models;

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetApplicationUrl() => GetBaseUrl().AppendPathSegment("/application");

    private IFlurlRequest GetApplicationsUrl() => GetBaseUrl().AppendPathSegment("/applications");

    public async Task<Application?> CreateApplicationAsync(object application)
    {
        var response = await GetApplicationUrl()
            .PostJsonAsync(application)
            .ConfigureAwait(false);

        return await HandleResponseAsync<Application>(response.ResponseMessage).ConfigureAwait(false);
    }

    // ReSharper disable once InconsistentNaming
    public async Task<IEnumerable<Application>> GetApplicationsAsync(bool _expand = false)
    {
        var url = GetApplicationsUrl();
        if (_expand)
        {
            url = url.SetQueryParam(nameof(_expand), _expand);
        }

        return await url
            .GetJsonAsync<IEnumerable<Application>>()
            .ConfigureAwait(false);
    }

    public async Task<Application> GetApplicationAsync(int id) => await GetApplicationUrl()
        .AppendPathSegment(id)
        .GetJsonAsync<Application>()
        .ConfigureAwait(false);

    public async Task<Application?> UpdateApplicationAsync(int id, object application)
    {
        var response = await GetApplicationUrl()
            .AppendPathSegment(id)
            .PutJsonAsync(application)
            .ConfigureAwait(false);

        return await HandleResponseAsync<Application>(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<Application?> DeleteApplicationAsync(int id)
    {
        var response = await GetApplicationUrl()
            .AppendPathSegment(id)
            .DeleteAsync()
            .ConfigureAwait(false);

        return await HandleResponseAsync<Application>(response.ResponseMessage).ConfigureAwait(false);
    }
}