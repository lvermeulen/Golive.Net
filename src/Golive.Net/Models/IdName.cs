using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class IdName
{
    public int? Id { get; set; }
    public string? Name { get; set; }

    public override string ToString() => this.PropertiesToString();
}