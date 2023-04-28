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
    public async Task CreateReadUpdateDeleteDeploymentAttributeAsync(string application, string category, int environmentId)
    {
        var deploymentAttribute = await _client.UpdateDeploymentAttributeAsync(application, category, environmentId, new
        {
            versionName = "ECOM 1.2.4",
            versionId = 101002,
            buildNumber = 2839948,
            description = "Release candidate for Summer 2020",
            orderedAttributes = Enumerable.Repeat(new OrderedAttribute { Key = "OS", Type = "STRING", Secured = false }, 1),
            linkedIssues = Enumerable.Repeat("string", 1)
        }).ConfigureAwait(false);

        DeploymentAttribute? result = null;
        var id = 0;
        try
        {
            result = await _client.GetDeploymentAttributeAsync(deploymentAttribute?.Id ?? -1).ConfigureAwait(false);
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(deploymentAttribute);
            id = result.Id ?? -1;
        }
        finally
        {
            if (result is not null)
            {
                result = await _client.DeleteDeploymentAttributeAsync(id).ConfigureAwait(false);
                Assert.NotNull(result);
            }
        }
    }

    [Fact]
    public async Task GetDeploymentAttributesAsync()
    {
        var results = await _client.GetDeploymentAttributesAsync().ConfigureAwait(false);
        Assert.NotNull(results);

        foreach (var result in results)
        {
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}