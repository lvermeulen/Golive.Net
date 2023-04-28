using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Golive.Net.Extensions;
using Golive.Net.Models;

namespace Golive.Net.Converters;

public class ErrorsConverter : JsonConverter<Errors>
{
    public override Errors? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);

        var firstProperty = doc.RootElement.Get(0);
        if (firstProperty is null)
        {
            return null;
        }

        return new Errors
        {
            Code = firstProperty.Value.Name,
            Message = firstProperty.Value.Value.ToString()
        };
    }

    public override void Write(Utf8JsonWriter writer, Errors? value, JsonSerializerOptions options) => throw new NotImplementedException();
}