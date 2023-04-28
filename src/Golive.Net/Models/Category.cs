using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class Category : IdName
{
    public int? Order { get; set; }

    public override string ToString() => this.PropertiesToString();
}