using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Golive.Net.Models;

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetCategoryUrl() => GetBaseUrl().AppendPathSegment("/category");

    private IFlurlRequest GetCategoriesUrl() => GetBaseUrl().AppendPathSegment("/categories");

    public async Task<Category?> CreateCategoryAsync(object category)
    {
        var response = await GetCategoryUrl()
            .PostJsonAsync(category)
            .ConfigureAwait(false);

        return await HandleResponseAsync<Category>(response.ResponseMessage).ConfigureAwait(false);
    }

    // ReSharper disable once InconsistentNaming
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await GetCategoriesUrl()
            .GetJsonAsync<IEnumerable<Category>>()
            .ConfigureAwait(false);
    }

    public async Task<Category> GetCategoryAsync(int id) => await GetCategoryUrl()
        .AppendPathSegment(id)
        .GetJsonAsync<Category>()
        .ConfigureAwait(false);

    public async Task<Category?> UpdateCategoryAsync(int id, object category)
    {
        var response = await GetCategoryUrl()
            .AppendPathSegment(id)
            .PutJsonAsync(category)
            .ConfigureAwait(false);

        return await HandleResponseAsync<Category>(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<bool> SwapCategory(int from, int to)
    {
        var response = await GetCategoryUrl()
            .AppendPathSegment("/swap")
            .PutJsonAsync(new { from, to })
            .ConfigureAwait(false);

        return await HandleResponseAsync(response.ResponseMessage).ConfigureAwait(false);
    }

    public async Task<Category?> DeleteCategoryAsync(int id)
    {
        var response = await GetCategoryUrl()
            .AppendPathSegment(id)
            .DeleteAsync()
            .ConfigureAwait(false);

        return await HandleResponseAsync<Category>(response.ResponseMessage).ConfigureAwait(false);
    }
}