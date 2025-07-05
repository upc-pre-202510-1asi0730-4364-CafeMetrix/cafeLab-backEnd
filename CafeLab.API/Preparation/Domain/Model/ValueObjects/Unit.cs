namespace CafeLab.API.Preparation.Domain.Model.ValueObjects;

/// <summary>
/// Unit Value Object
/// </summary>
public record Unit
{
    public string Value { get; }

    public Unit()
    {
        Value = string.Empty;
    }

    public Unit(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Unit cannot be null or empty");

        if (!IsValidUnit(value))
            throw new ArgumentException($"Invalid unit: {value}");

        Value = value.Trim().ToLower();
    }

    private static bool IsValidUnit(string value)
    {
        var validUnits = new[] { "gr", "ml", "oz", "unidad" };
        return validUnits.Contains(value.Trim().ToLower());
    }

    public static implicit operator string(Unit unit) => unit.Value;
    public static implicit operator Unit(string value) => new(value);

    public override string ToString() => Value;
} 