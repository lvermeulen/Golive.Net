using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Golive.Net.Models;
using Xunit;

namespace Golive.Net.Tests;

public partial class GoliveClientShould
{
    [Theory]
    [InlineData("streetname-registry", "Test", 4)]
    public async Task CreateReadUpdateDeleteEnvironmentAttributeAsync(string application, string category, int environmentId)
    {
        var environmentAttribute = await _client.UpdateEnvironmentAttributeAsync(application, category, environmentId, new
        {
            versionName = "ECOM 1.2.4",
            versionId = 101002,
            buildNumber = 2839948,
            description = "Release candidate for Summer 2020",
            orderedAttributes = Enumerable.Repeat(new OrderedAttribute { Key = "OS", Type = "STRING", Secured = false }, 1),
            linkedIssues = Enumerable.Repeat("string", 1)
        }).ConfigureAwait(false);

        OrderedAttribute? result = null;
        var id = 0;
        try
        {
            result = await _client.GetEnvironmentAttributeAsync(environmentAttribute?.Id ?? -1).ConfigureAwait(false);
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(environmentAttribute);
            id = result.Id ?? -1;
        }
        finally
        {
            if (result is not null)
            {
                result = await _client.DeleteEnvironmentAttributeAsync(id).ConfigureAwait(false);
                Assert.NotNull(result);
            }
        }
    }

    [Fact]
    public async Task GetEnvironmentAttributesAsync()
    {
        var results = await _client.GetEnvironmentAttributesAsync().ConfigureAwait(false);
        Assert.NotNull(results);

        foreach (var result in results)
        {
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}