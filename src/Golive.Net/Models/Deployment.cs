using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Golive.Net.Converters;
using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class Deployment
{
    public int? DeploymentId { get; set; }
    public int? Id { get; set; }
    public int? EnvironmentId { get; set; }
    public string? VersionName { get; set; }
    public string? VersionId { get; set; }
    public string? Deployer { get; set; }

    [JsonConverter(typeof(UnixToNullableDateTimeConverter))]
    public DateTime? DeployedTime { get; set; }
    
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int? ApplicationId { get; set; }
    public string? ApplicationName { get; set; }

    [JsonConverter(typeof(UnixToNullableDateTimeConverter))]
    public DateTime? EndTime { get; set; }

    public long? Duration { get; set; }
    public string? BuildNumber { get; set; }
    public string? Description { get; set; }
    public IEnumerable<OrderedAttribute>? OrderedAttributes { get; set; }
    public IEnumerable<string>? LinkedIssues { get; set; }
    public Attributes? Attributes { get; set; }

    public override string ToString() => this.PropertiesToString();
}
