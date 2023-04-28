using System.Threading.Tasks;
using Xunit;

namespace Golive.Net.Tests;

public partial class GoliveClientShould
{
    [Fact]
    public async Task GetWatcherStatisticsAsync()
    {
        var result = await _client.GetWatcherStatisticsAsync().ConfigureAwait(false);
        Assert.NotNull(result);

        _testOutputHelper.WriteLine(result.ToString());
    }

    [Fact]
    public async Task GetWatcherByEnvironmentAsync()
    {
        var result = await _client.GetWatcherByEnvironmentAsync(4).ConfigureAwait(false);
        Assert.NotNull(result);

        _testOutputHelper.WriteLine(result.ToString());
    }
}