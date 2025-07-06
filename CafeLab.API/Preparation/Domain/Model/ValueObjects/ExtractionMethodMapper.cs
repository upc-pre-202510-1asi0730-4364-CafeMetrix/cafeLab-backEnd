namespace CafeLab.API.Preparation.Domain.Model.ValueObjects;

/// <summary>
/// Static mapper for ExtractionMethod conversions following ACME patterns
/// </summary>
public static class ExtractionMethodMapper
{
    /// <summary>
    /// Converts frontend string values to EExtractionMethod enum
    /// </summary>
    /// <param name="frontendValue">The string value from frontend (e.g., "french-press")</param>
    /// <returns>The corresponding EExtractionMethod enum value</returns>
    /// <exception cref="ArgumentException">Thrown when the value is not recognized</exception>
    public static EExtractionMethod FromFrontendValue(string frontendValue)
    {
        return frontendValue.ToLower() switch
        {
            "french-press" => EExtractionMethod.FrenchPress,
            "cold-brew" => EExtractionMethod.ColdBrew,
            "pour-over" => EExtractionMethod.PourOver,
            "aeropress" => EExtractionMethod.Aeropress,
            "chemex" => EExtractionMethod.Chemex,
            "espresso" => EExtractionMethod.Espresso,
            _ => throw new ArgumentException($"Invalid extraction method: {frontendValue}")
        };
    }

    /// <summary>
    /// Converts EExtractionMethod enum to frontend string values
    /// </summary>
    /// <param name="method">The EExtractionMethod enum value</param>
    /// <returns>The corresponding frontend string value (e.g., "french-press")</returns>
    /// <exception cref="ArgumentException">Thrown when the enum value is not recognized</exception>
    public static string ToFrontendValue(EExtractionMethod method)
    {
        return method switch
        {
            EExtractionMethod.FrenchPress => "french-press",
            EExtractionMethod.ColdBrew => "cold-brew",
            EExtractionMethod.PourOver => "pour-over",
            EExtractionMethod.Aeropress => "aeropress",
            EExtractionMethod.Chemex => "chemex",
            EExtractionMethod.Espresso => "espresso",
            _ => throw new ArgumentException($"Unknown extraction method: {method}")
        };
    }
} 