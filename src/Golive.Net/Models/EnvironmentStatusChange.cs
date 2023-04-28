using System;
using System.Text.Json.Serialization;
using Golive.Net.Converters;
using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class EnvironmentStatusChange
{
    public int? Id { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int? ApplicationId { get; set; }
    public string? ApplicationName { get; set; }
    public string? StatusName { get; set; }
    public EnvironmentStatus? Status { get; set; }
    public string? ChangedBy { get; set; }

    [JsonConverter(typeof(UnixToNullableDateTimeConverter))]
    public DateTime? ChangedOn { get; set; }

    [JsonConverter(typeof(UnixToNullableDateTimeConverter))]
    public DateTime? EndTime { get; set; }
    
    public long? Duration { get; set; }

    public override string ToString() => this.PropertiesToString();
}