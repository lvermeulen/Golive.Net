using System.Threading.Tasks;
using FluentAssertions;
using Golive.Net.Models;
using Xunit;

namespace Golive.Net.Tests;

public partial class GoliveClientShould
{
    [Fact]
    public async Task CreateReadUpdateDeleteApplicationAsync()
    {
        var application = await _client.CreateApplicationAsync(new
        {
            name = nameof(CreateReadUpdateDeleteApplicationAsync),
            versionPrefix = "CRUD",
            mappedProjectId = 10,
            createNewVersion = true,
            deploymentConfiguration = new 
            {
                showBuildNumber = true,
                showDescription = true,
                preventDeploymentMerge = true
            }
        }).ConfigureAwait(false);

        Application? result = null;
        var id = 0;
        try
        {
            result = await _client.GetApplicationAsync(application?.Id ?? -1).ConfigureAwait(false);
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(application);
            id = result.Id ?? -1;

            const string versionPrefix = "STH";
            result.VersionPrefix = versionPrefix;
            result = await _client.UpdateApplicationAsync(id, result).ConfigureAwait(false);
            Assert.NotNull(result);
        }
        finally
        {
            if (result is not null)
            {
                result = await _client.DeleteApplicationAsync(id).ConfigureAwait(false);
                Assert.NotNull(result);
            }
        }
    }

    [Fact]
    public async Task GetApplicationsAsync()
    {
        var results = await _client.GetApplicationsAsync().ConfigureAwait(false);
        Assert.NotNull(results);

        foreach (var result in results)
        {
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}