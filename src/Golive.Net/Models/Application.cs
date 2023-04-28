using System.Collections.Generic;
using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class Application
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? VersionPrefix { get; set; }
    public int? MappedProjectId { get; set; }
    public bool? CreateNewVersion { get; set; }
    public DeploymentConfiguration? DeploymentConfiguration { get; set; }
    public ApplicationScheme? ApplicationScheme { get; set; }
    public IEnumerable<DeploymentAttribute>? DeploymentAttributes { get; set; }
    public Attributes? Attributes { get; set; }

    public override string ToString() => this.PropertiesToString();
}