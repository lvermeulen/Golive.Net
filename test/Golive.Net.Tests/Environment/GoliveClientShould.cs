using System.Threading.Tasks;
using FluentAssertions;
using Golive.Net.Models;
using Xunit;

namespace Golive.Net.Tests;

public partial class GoliveClientShould
{
    [Theory]
    [InlineData(nameof(GoliveClientShould), "test")]
    public async Task CreateReadUpdateDeleteEnvironmentAsync(string application, string category)
    {
        var app = await _client.CreateApplicationAsync(new { name = application }).ConfigureAwait(false);
        try
        {
            var environment = await _client.CreateEnvironmentAsync(new
                {
                    application = new { name = application },
                    category = new { name = category },
                    environmentPermissionScheme = new { id = 2 },
                    url = "https://some.url",
                    attributes = new
                    {
                        OS = "RedHat 7",
                        database = "postgres",
                        location = "EU"
                    }
                }
            ).ConfigureAwait(false);

            Environment? result = null;
            try
            {
                result = await _client.GetEnvironmentAsync(environment?.Id ?? -1).ConfigureAwait(false);
                Assert.NotNull(result);
                result.Should().BeEquivalentTo(environment);

                const string url = "https://some.other.url";
                result.Url = url;
                result = await _client.UpdateEnvironmentAsync(result.Id ?? -1, result).ConfigureAwait(false);
                Assert.NotNull(result);
            }
            finally
            {
                if (result is not null)
                {
                    result = await _client.DeleteEnvironmentAsync(result.Id ?? -1).ConfigureAwait(false);
                    Assert.NotNull(result);
                }
            }
        }
        finally
        {
            await _client.DeleteApplicationAsync(app?.Id ?? -1).ConfigureAwait(false);
        }
    }

    [Theory]
    [InlineData("streetname-registry", "test")]
    public async Task GetEnvironmentsAsync(string application, string category)
    {
        var result = await _client.GetEnvironmentsAsync(application, category).ConfigureAwait(false);
        Assert.NotNull(result);
    }
}