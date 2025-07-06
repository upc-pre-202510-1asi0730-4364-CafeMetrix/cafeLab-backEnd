namespace CafeLab.API.IAM.Domain.Model.ValueObjects;

/// <summary>
/// Email address value object
/// 
/// Este Value Object encapsula la lógica de validación y manipulación
/// de direcciones de correo electrónico en el sistema.
/// 
/// Características:
/// - Validación automática del formato de email
/// - Normalización a minúsculas
/// - Inmutabilidad (no se puede modificar después de la creación)
/// - Comparación por valor, no por referencia
/// 
/// Los Value Objects son fundamentales en DDD para encapsular conceptos
/// del dominio que no tienen identidad propia.
/// </summary>
public class EmailAddress
{
    public string Address { get; private set; }

    public EmailAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email address cannot be empty", nameof(address));

        if (!IsValidEmail(address))
            throw new ArgumentException("Invalid email format", nameof(address));

        Address = address.ToLowerInvariant();
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public override string ToString()
    {
        return Address;
    }

    public override bool Equals(object? obj)
    {
        if (obj is EmailAddress other)
            return Address.Equals(other.Address, StringComparison.OrdinalIgnoreCase);
        return false;
    }

    public override int GetHashCode()
    {
        return Address.ToLowerInvariant().GetHashCode();
    }
} 