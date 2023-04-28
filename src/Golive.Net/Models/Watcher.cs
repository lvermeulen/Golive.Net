using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class Watcher
{
    public int? Id { get; set; }
    public string? UserKey { get; set; }
    public string? UserName { get; set; }
    public int? EnvironmentId { get; set; }
    public bool? WatchEnvironmentChanged { get; set; }
    public bool? WatchDeployedVersion { get; set; }
    public bool? WatchStatus { get; set; }
    public bool? WatchIssueAdded { get; set; }
    public bool? WatchIssueRemoved { get; set; }

    public override string ToString() => this.PropertiesToString();
}
