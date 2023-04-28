using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class EnvironmentPermissionScheme
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Global { get; set; }

    public override string ToString() => this.PropertiesToString();
}