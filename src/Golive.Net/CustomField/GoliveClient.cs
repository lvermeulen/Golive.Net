using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Golive.Net.Models;

// ReSharper disable InconsistentNaming

namespace Golive.Net;

public partial class GoliveClient
{
    private IFlurlRequest GetCustomFieldsSelectableUrl() => GetBaseUrl().AppendPathSegment("/customfield/environment/selectable");

    private IFlurlRequest GetCustomFieldsSelectedUrl() => GetBaseUrl().AppendPathSegment("/customfield/environment/selected");

    public async Task<IEnumerable<CustomFieldSelectable>?> GetCustomFieldsSelectableAsync(string? projectKey = null, int? projectId = null,
        string? customfieldKey = null, string? customfieldName = null, IEnumerable<int>? environmentId = null, IEnumerable<string>? environmentName = null,
        IEnumerable<string>? applicationName = null, IEnumerable<int>? applicationId = null, IEnumerable<string>? categoryName = null, IEnumerable<int>? categoryId = null,
        IEnumerable<string>? statusName = null, IEnumerable<int>? statusId = null, IEnumerable<string>? permissionSchemeName = null, IEnumerable<int>? permissionSchemeId = null,
        int? _limit = null, int? _offset = null, string? _sortOrder = null, string? _sortBy = null, bool? _expand = null)
    {
        static void AddQueryParams<TItem>(List<(string, object?)> dictionary, IEnumerable<TItem>? items, string name)
        {
            dictionary.AddRange((items ?? Enumerable.Empty<TItem>())
                .Select(x => (name, x))
                .Select(x => ((string, object?))x));
        }

        var queryParams = new List<(string, object?)>
        {
            (nameof(projectKey), projectKey),
            (nameof(projectId), projectId),
            (nameof(customfieldKey), customfieldKey),
            (nameof(customfieldName), customfieldName),
            (nameof(_limit), _limit),
            (nameof(_offset), _offset),
            (nameof(_sortOrder), _sortOrder),
            (nameof(_sortBy), _sortBy),
            (nameof(_expand), _expand)
        };

        AddQueryParams(queryParams, environmentId, nameof(environmentId));
        AddQueryParams(queryParams, environmentName, nameof(environmentName));
        AddQueryParams(queryParams, applicationName, nameof(applicationName));
        AddQueryParams(queryParams, applicationId, nameof(applicationId));
        AddQueryParams(queryParams, categoryName, nameof(categoryName));
        AddQueryParams(queryParams, categoryId, nameof(categoryId));
        AddQueryParams(queryParams, statusName, nameof(statusName));
        AddQueryParams(queryParams, statusId, nameof(statusId));
        AddQueryParams(queryParams, permissionSchemeName, nameof(permissionSchemeName));
        AddQueryParams(queryParams, permissionSchemeId, nameof(permissionSchemeId));

        var response = await GetCustomFieldsSelectableUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .GetAsync()
            .ConfigureAwait(false);

        return await HandleResponseFirstNodeAsync<IEnumerable<CustomFieldSelectable>>(response.ResponseMessage);
    }

    public async Task<IEnumerable<CustomFieldSelected>?> GetCustomFieldsSelectedAsync(string issueKey, string? customfieldKey = null, string? customfieldName = null)
    {
        var queryParams = new Dictionary<string, object?>
        {
            [nameof(issueKey)] = issueKey,
            [nameof(customfieldKey)] = customfieldKey,
            [nameof(customfieldName)] = customfieldName
        };

        var response = await GetCustomFieldsSelectedUrl()
            .SetQueryParams(queryParams, NullValueHandling.Ignore)
            .GetAsync()
            .ConfigureAwait(false);

        return await HandleResponseFirstNodeAsync<IEnumerable<CustomFieldSelected>>(response.ResponseMessage);
    }
}