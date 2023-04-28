using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class Environment
{
    public int? Id { get; set; }
    public Application? Application { get; set; }
    public Category? Category { get; set; }
    public EnvironmentPermissionScheme? EnvironmentPermissionScheme { get; set; }
    public string? UrlHtml { get; set; }
    public bool? Watched { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public Attributes? Attributes { get; set; }

    public override string ToString() => this.PropertiesToString();
}