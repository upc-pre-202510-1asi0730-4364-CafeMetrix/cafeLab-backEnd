using System.Text.RegularExpressions;

namespace CafeLab.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
///     Converts the text to kebab case.
/// </summary>
/// <param>string to convert</param>
/// <returns>
///     The kebab case string.
/// </returns>
public static partial class StringExtensions
{
    public static string ToKebabCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        return KebabCaseRegex().Replace(text, "-$1")
            .Trim()
            .ToLower();
    }

    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex KebabCaseRegex();
}