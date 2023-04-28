using System.Collections.Generic;

namespace Golive.Net.Models;

public class Error
{
    public IEnumerable<string>? Warnings { get; set; }
    public IEnumerable<string>? ErrorMessages { get; set; }
    public Errors? Errors { get; set; }
    public int? Status { get; set; }
}