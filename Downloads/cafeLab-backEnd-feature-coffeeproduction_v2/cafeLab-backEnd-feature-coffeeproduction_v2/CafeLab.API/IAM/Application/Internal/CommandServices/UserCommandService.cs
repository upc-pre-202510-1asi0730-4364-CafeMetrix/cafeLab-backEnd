using CafeLab.API.IAM.Domain.Model.Aggregates;
using CafeLab.API.IAM.Domain.Model.Commands;
using CafeLab.API.IAM.Domain.Model.ValueObjects;
using CafeLab.API.IAM.Domain.Repositories;
using CafeLab.API.IAM.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.IAM.Application.Internal.CommandServices;

/// <summary>
/// User command service implementation
/// 
/// Este servicio implementa la lógica de negocio para las operaciones de comando
/// relacionadas con usuarios en el bounded context IAM.
/// 
/// Responsabilidades:
/// - Crear nuevos usuarios con validaciones de negocio
/// - Gestionar el proceso de autenticación (sign-in)
/// - Validar credenciales y generar tokens JWT
/// - Mantener la integridad de los datos de usuario
/// 
/// Patrón CQRS: Este servicio maneja los comandos (operaciones de escritura)
/// </summary>
public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;

    public UserCommandService(IUserRepository userRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Crea un nuevo usuario en el sistema
    /// 
    /// Este método implementa la lógica de negocio para la creación de usuarios,
    /// incluyendo validaciones de unicidad de email y username, y el hashing
    /// automático de la contraseña.
    /// </summary>
    /// <param name="command">Comando con los datos del usuario a crear</param>
    /// <returns>El usuario creado</returns>
    /// <exception cref="InvalidOperationException">Si el email o username ya existen</exception>
    public async Task<User> CreateAsync(CreateUserCommand command)
    {
        // Validar que el email no esté ya registrado
        var email = new EmailAddress(command.Email);
        if (await _userRepository.ExistsByEmailAsync(email))
            throw new InvalidOperationException($"Email {command.Email} is already registered");

        // Validar que el username no esté ya tomado
        if (await _userRepository.ExistsByUsernameAsync(command.Username))
            throw new InvalidOperationException($"Username {command.Username} is already taken");

        // Crear el usuario (la entidad se encarga del hashing de la contraseña)
        var user = new User(command);
        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        return user;
    }

    /// <summary>
    /// Autentica un usuario y genera un token JWT
    /// 
    /// Este método implementa el proceso completo de autenticación:
    /// 1. Busca el usuario por email
    /// 2. Valida la contraseña usando BCrypt
    /// 3. Verifica que la cuenta esté activa
    /// 4. Actualiza la fecha de último login
    /// 5. Genera y retorna un token JWT
    /// </summary>
    /// <param name="command">Comando con las credenciales de autenticación</param>
    /// <returns>Token JWT para el usuario autenticado</returns>
    /// <exception cref="InvalidOperationException">Si las credenciales son inválidas o la cuenta está desactivada</exception>
    public async Task<string> SignInAsync(SignInCommand command)
    {
        // Buscar usuario por email
        var email = new EmailAddress(command.Email);
        var user = await _userRepository.GetByEmailAsync(email);

        // Validar que el usuario exista
        if (user == null)
            throw new InvalidOperationException("Invalid email or password");

        // Validar contraseña usando BCrypt
        if (!user.ValidatePassword(command.Password))
            throw new InvalidOperationException("Invalid email or password");

        // Verificar que la cuenta esté activa
        if (!user.IsActive)
            throw new InvalidOperationException("User account is deactivated");

        // Actualizar fecha de último login
        user.UpdateLastLogin();
        await _userRepository.UpdateAsync(user);
        await _unitOfWork.CompleteAsync();

        // Generar y retornar token JWT
        return _jwtService.GenerateToken(user);
    }
} 