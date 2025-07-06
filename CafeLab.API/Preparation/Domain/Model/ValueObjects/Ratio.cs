namespace CafeLab.API.Preparation.Domain.Model.ValueObjects;

/// <summary>
/// Ratio Value Object
/// </summary>
public record Ratio
{
    public string Value { get; }

    public Ratio()
    {
        Value = string.Empty;
    }

    public Ratio(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Ratio cannot be null or empty");

        if (!IsValidRatio(value))
            throw new ArgumentException($"Invalid ratio: {value}");

        Value = value.Trim();
    }

    private static bool IsValidRatio(string value)
    {
        var validRatios = new[] { "1:1", "1:2", "1:3", "1:4", "1:12", "1:14", "1:16", "1:18" };
        return validRatios.Contains(value.Trim());
    }

    public static implicit operator string(Ratio ratio) => ratio.Value;
    public static implicit operator Ratio(string value) => new(value);

    public override string ToString() => Value;
} 