using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class DeploymentConfiguration
{
    public bool? ShowBuildNumber { get; set; }
    public bool? ShowDescription { get; set; }
    public bool? PreventDeploymentMerge { get; set; }

    public override string ToString() => this.PropertiesToString();
}