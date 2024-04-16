using System.Text.RegularExpressions;

namespace Vadalo;

internal static partial class PartialExtensions
{
    [
        GeneratedRegex(
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant
        )
    ]
    internal static partial Regex EmailRegex();
}