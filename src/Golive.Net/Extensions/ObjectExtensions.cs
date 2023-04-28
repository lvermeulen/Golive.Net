using System.Linq;

namespace Golive.Net.Extensions
{
    internal static class ObjectExtensions
    {
        internal static string PropertiesToString(this object obj) => string.Join(", ", obj
            .GetType()
            .GetProperties()
            .Select(x => $"{x.Name}: {x.GetValue(obj)}"));
    }
}
