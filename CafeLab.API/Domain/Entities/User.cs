using System;
using System.Collections.Generic;
using CafeLab.API.Shared.Entities;

namespace CafeLab.API.Domain.Entities;

/// <summary>
/// Representa un usuario en el sistema.
/// Esta entidad es el agregado raíz para la gestión de usuarios.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Nombre de usuario único para el inicio de sesión.
    /// </summary>
    public string Username { get; private set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    public string Email { get; private set; } = string.Empty;

    /// <summary>
    /// Contraseña hasheada del usuario.
    /// </summary>
    public string PasswordHash { get; private set; } = string.Empty;

    /// <summary>
    /// Nombre completo del usuario.
    /// </summary>
    public string FullName { get; private set; } = string.Empty;

    /// <summary>
    /// Rol del usuario en el sistema (por ejemplo: Admin, Catador, Usuario).
    /// </summary>
    public string Role { get; private set; } = string.Empty;

    /// <summary>
    /// Indica si el usuario está activo en el sistema.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Fecha del último inicio de sesión del usuario.
    /// </summary>
    public DateTime? LastLoginDate { get; private set; }

    // EF Core needs a parameterless constructor for migrations
    private User() { }

    public User(string username, string email, string passwordHash, string fullName, string role)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username cannot be empty.", nameof(username));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.", nameof(email));
        // Add more validations (e.g., email format) as needed

        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        FullName = fullName;
        Role = role;
        IsActive = true;
    }

    public void SetPassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    public void SetLastLoginDate()
    {
        LastLoginDate = DateTime.UtcNow;
    }
} 