using CafeLab.API.IAM.Domain.Model.Commands;
using CafeLab.API.IAM.Domain.Model.Queries;
using CafeLab.API.IAM.Domain.Services;
using CafeLab.API.IAM.Interfaces.REST.Resources;
using CafeLab.API.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace CafeLab.API.IAM.Interfaces.REST;

/// <summary>
/// Users controller for IAM operations
/// 
/// Este controlador maneja todas las operaciones HTTP relacionadas con usuarios
/// en el bounded context IAM (Identity and Access Management).
/// 
/// Endpoints disponibles:
/// - POST /sign-up: Registro de nuevos usuarios
/// - POST /sign-in: Autenticación de usuarios
/// - GET /{id}: Obtener información de un usuario específico
/// - GET /: Listar todos los usuarios
/// 
/// El controlador utiliza los servicios de aplicación para implementar
/// la lógica de negocio y mantiene la separación de responsabilidades.
/// </summary>
[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;
    private readonly IUserQueryService _userQueryService;

    public UsersController(IUserCommandService userCommandService, IUserQueryService userQueryService)
    {
        _userCommandService = userCommandService;
        _userQueryService = userQueryService;
    }

    /// <summary>
    /// Create a new user (Sign Up)
    /// </summary>
    /// <param name="resource">User creation data</param>
    /// <returns>Created user information</returns>
    [HttpPost("sign-up")]
    public async Task<ActionResult<UserResource>> SignUp([FromBody] CreateUserResource resource)
    {
        try
        {
            var command = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
            var user = await _userCommandService.CreateAsync(command);
            var responseResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, responseResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Authenticate user (Sign In)
    /// </summary>
    /// <param name="resource">Sign in credentials</param>
    /// <returns>JWT token and user information</returns>
    [HttpPost("sign-in")]
    public async Task<ActionResult<SignInResponseResource>> SignIn([FromBody] SignInResource resource)
    {
        try
        {
            var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
            var token = await _userCommandService.SignInAsync(command);
            
            // Get user information for response
            var userQuery = new GetUserByEmailQuery(new CafeLab.API.IAM.Domain.Model.ValueObjects.EmailAddress(resource.Email));
            var user = await _userQueryService.GetByEmailAsync(userQuery);
            
            if (user == null)
                return BadRequest("User not found");

            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
            var response = new SignInResponseResource(token, userResource);
            
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User information</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResource>> GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _userQueryService.GetByIdAsync(query);
        
        if (user == null)
            return NotFound();

        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(resource);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>List of all users</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResource>>> GetAll()
    {
        var query = new GetAllUsersQuery();
        var users = await _userQueryService.GetAllAsync(query);
        var resources = users.Select(u => UserResourceFromEntityAssembler.ToResourceFromEntity(u));
        return Ok(resources);
    }
} 