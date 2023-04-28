using System.Text.Json;
using System.Text.Json.Serialization;
using Golive.Net.Converters;
using Golive.Net.Models;
using Xunit;

namespace Golive.Net.Tests.Converters;

public class ErrorsConverterShould
{
    [Fact]
    public void ConvertError()
    {
        var json = @"{
    'warnings': [],
    'errorMessages': [],
    'errors': {'NAME':'Name cannot be empty'},
    'status': 500
}".Replace("'", "\"");

        var result = JsonSerializer.Deserialize<Error>(json, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new ErrorsConverter() }
        });
        Assert.NotNull(result);
        Assert.NotNull(result.Errors);
    }
}