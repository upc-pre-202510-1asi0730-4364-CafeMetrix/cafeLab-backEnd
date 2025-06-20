namespace CafeLab.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Convierte una cadena PascalCase o camelCase a kebab-case.
    /// </summary>
    public static string ToKebabCase(this string str)
    {
        if (string.IsNullOrEmpty(str)) return str;
        var chars = new List<char>();
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if (char.IsUpper(c))
            {
                if (i > 0) chars.Add('-');
                chars.Add(char.ToLower(c));
            }
            else
            {
                chars.Add(c);
            }
        }
        return new string(chars.ToArray());
    }
} 