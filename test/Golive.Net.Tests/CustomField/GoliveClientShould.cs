using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Golive.Net.Tests;

public partial class GoliveClientShould
{
    [Theory]
    [InlineData("TG", "apwide-environment-custom-field-1")]
    public async Task GetCustomFieldsSelectableAsync(string projectKey, string customfieldKey)
    {
        var results = await _client.GetCustomFieldsSelectableAsync(projectKey: projectKey, customfieldKey: customfieldKey, environmentId: new List<int> { 4, 5 }).ConfigureAwait(false);
        Assert.NotNull(results);

        foreach (var result in results)
        {
            _testOutputHelper.WriteLine(result.ToString());
        }
    }

    [Theory]
    [InlineData("TG-1", "apwide-environment-custom-field-1")]
    public async Task GetCustomFieldsSelectedAsync(string issueKey, string customfieldKey)
    {
        var results = await _client.GetCustomFieldsSelectedAsync(issueKey, customfieldKey: customfieldKey).ConfigureAwait(false);
        Assert.NotNull(results);

        foreach (var result in results)
        {
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}