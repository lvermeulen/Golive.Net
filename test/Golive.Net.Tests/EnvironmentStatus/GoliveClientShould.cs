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
    public async Task CreateReadUpdateDeleteEnvironmentStatusChangeAsync(string application, string category, int environmentId)
    {
        var environmentStatus = await _client.UpdateEnvironmentStatusChangeAsync(application, category, environmentId, new
        {
            versionName = "ECOM 1.2.4",
            versionId = 101002,
            buildNumber = 2839948,
            description = "Release candidate for Summer 2020",
            EnvironmentStatuss = Enumerable.Repeat(new EnvironmentStatus { Name = "OS", Description = "STRING" }, 1),
            linkedIssues = Enumerable.Repeat("string", 1)
        }).ConfigureAwait(false);

        EnvironmentStatusChange? result = null;
        var id = 0;
        try
        {
            result = await _client.GetEnvironmentStatusChangeAsync(environmentStatus?.Id ?? -1).ConfigureAwait(false);
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(environmentStatus);
            id = result.Id ?? -1;
        }
        finally
        {
            if (result is not null)
            {
                result = await _client.DeleteEnvironmentStatusChangeAsync(id).ConfigureAwait(false);
                Assert.NotNull(result);
            }
        }
    }

    [Fact]
    public async Task GetEnvironmentStatusChangesAsync()
    {
        var results = await _client.GetEnvironmentStatusChangesAsync().ConfigureAwait(false);
        Assert.NotNull(results);

        foreach (var result in results)
        {
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}