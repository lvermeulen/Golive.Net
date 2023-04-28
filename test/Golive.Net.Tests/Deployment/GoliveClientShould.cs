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
    public async Task CreateReadUpdateDeleteDeploymentAsync(string application, string category, int environmentId)
    {
        var deployment = await _client.UpdateDeploymentAsync(application, category, environmentId, new
        {
            versionName = "ECOM 1.2.4",
            versionId = 101002,
            buildNumber = 2839948,
            description = "Release candidate for Summer 2020",
            orderedAttributes = Enumerable.Repeat(new OrderedAttribute { Key = "OS", Type = "STRING", Secured = false }, 1),
            linkedIssues = Enumerable.Repeat("string", 1)
        }).ConfigureAwait(false);

        Deployment? result = null;
        var id = 0;
        try
        {
            result = await _client.GetDeploymentAsync(deployment?.Id ?? -1).ConfigureAwait(false);
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(deployment);
            id = result.Id ?? -1;
        }
        finally
        {
            if (result is not null)
            {
                result = await _client.DeleteDeploymentAsync(id).ConfigureAwait(false);
                Assert.NotNull(result);
            }
        }
    }

    [Theory]
    [InlineData("streetname-registry", "Test", 4)]
    public async Task GetDeploymentsAsync(string application, string category, int environmentId)
    {
        var results = await _client.GetDeploymentsAsync(application, category, environmentId).ConfigureAwait(false);
        Assert.NotNull(results);

        foreach (var result in results)
        {
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}