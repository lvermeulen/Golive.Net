using System.IO;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace Golive.Net.Tests;

public partial class GoliveClientShould
{
    private readonly GoliveClient _client;
    private readonly ITestOutputHelper _testOutputHelper;

    public GoliveClientShould(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets("9eb592dd-d219-4dda-a3b5-62ce569569c5")
            .Build();

        var url = configuration["url"];
        var apiKey = configuration["apiKey"];

        _client = new GoliveClient(url, apiKey);
    }
}