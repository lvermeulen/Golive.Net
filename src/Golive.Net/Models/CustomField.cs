using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class CustomFieldSelected : IdName
{
    public IdName? Application { get; set; }
    public IdName? Category { get; set; }

    public override string ToString() => this.PropertiesToString();
}