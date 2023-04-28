using System.Threading.Tasks;
using FluentAssertions;
using Golive.Net.Models;
using Xunit;

namespace Golive.Net.Tests;

public partial class GoliveClientShould
{
    [Fact]
    public async Task CreateReadUpdateDeleteCategoryAsync()
    {
        var category = await _client.CreateCategoryAsync(new
        {
            name = nameof(CreateReadUpdateDeleteCategoryAsync),
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

        Category? result = null;
        var id = 0;
        try
        {
            result = await _client.GetCategoryAsync(category?.Id ?? -1).ConfigureAwait(false);
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(category);
            id = result.Id ?? -1;

            const string name = "STH";
            result.Name = name;
            result = await _client.UpdateCategoryAsync(id, result).ConfigureAwait(false);
            Assert.NotNull(result);
        }
        finally
        {
            if (result is not null)
            {
                result = await _client.DeleteCategoryAsync(id).ConfigureAwait(false);
                Assert.NotNull(result);
            }
        }
    }

    [Fact]
    public async Task GetCategoriesAsync()
    {
        var results = await _client.GetCategoriesAsync().ConfigureAwait(false);
        Assert.NotNull(results);

        foreach (var result in results)
        {
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}