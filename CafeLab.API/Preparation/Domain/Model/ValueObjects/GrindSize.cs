namespace CafeLab.API.Preparation.Domain.Model.ValueObjects;

/// <summary>
/// GrindSize Value Object
/// </summary>
public record GrindSize
{
    public string Value { get; }

    public GrindSize()
    {
        Value = string.Empty;
    }

    public GrindSize(string? value)
    {
        // Allow null or empty values for GrindSize
        if (string.IsNullOrWhiteSpace(value))
        {
            Value = string.Empty;
            return;
        }

        if (!IsValidGrindSize(value))
            throw new ArgumentException($"Invalid grind size: {value}. Valid values are: fino, medio, grueso");

        Value = value.Trim().ToLower();
    }

    private static bool IsValidGrindSize(string value)
    {
        var validSizes = new[] { "fino", "medio", "grueso" };
        return validSizes.Contains(value.Trim().ToLower());
    }

    public static implicit operator string(GrindSize grindSize) => grindSize.Value;
    public static implicit operator GrindSize(string? value) => new(value);

    public override string ToString() => Value;
} 