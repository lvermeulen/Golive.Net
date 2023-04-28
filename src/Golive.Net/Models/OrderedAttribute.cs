using System;
using System.Text.Json.Serialization;
using Golive.Net.Converters;
using Golive.Net.Extensions;

namespace Golive.Net.Models;

public class OrderedAttribute
{
    public int? Id { get; set; }
    public string? Key { get; set; }
    public string? Type { get; set; }
    public bool? Secured { get; set; }
    public string? CreatedBy { get; set; }

    [JsonConverter(typeof(UnixToNullableDateTimeConverter))]
    public DateTime? CreatedOn { get; set; }
    
    public string? ModifiedBy { get; set; }

    [JsonConverter(typeof(UnixToNullableDateTimeConverter))]
    public DateTime? ModifiedOn { get; set; }

    public override string ToString() => this.PropertiesToString();
}
