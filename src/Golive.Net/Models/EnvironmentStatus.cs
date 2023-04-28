using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class EnvironmentStatus
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public int? Order { get; set; }

    public override string ToString() => this.PropertiesToString();
}
