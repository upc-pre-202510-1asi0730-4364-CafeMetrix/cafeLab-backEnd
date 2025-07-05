using System.Net.Mime;
using CafeLab.API.Profiles.Domain.Model.Queries;
using CafeLab.API.Profiles.Domain.Services;
using CafeLab.API.Profiles.Interfaces.REST.Resources;
using CafeLab.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CafeLab.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile Endpoints.")]
public class ProfilesController(IProfileCommandService profileCommandService, IProfileQueryService profileQueryService) : ControllerBase
{
    // Filtro para manejar errores de binding de parámetros
    public class ValidateIntRouteParameterAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName;
        public ValidateIntRouteParameterAttribute(string parameterName)
        {
            _parameterName = parameterName;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue(_parameterName, out var value) || value is not int)
            {
                context.Result = new BadRequestObjectResult($"El parámetro '{_parameterName}' debe ser un número entero.");
            }
        }
    }

    [HttpGet("{profileId:int}")]
    [ValidateIntRouteParameter("profileId")]
    [SwaggerOperation("Get Profile by Id", "Get a profile by its unique identifier.", OperationId = "GetProfileById")]
    [SwaggerResponse(200, "The profile was found and returned.", typeof(ProfileResource))]
    [SwaggerResponse(404, "The profile was not found.")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if (profile is null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Profile", "Create a new profile.", OperationId = "CreateProfile")]
    [SwaggerResponse(201, "The profile was created.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile was not created.")]
    public async Task<IActionResult> CreateProfile(CreateProfileResource resource)
    {
        var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var profile = await profileCommandService.Handle(createProfileCommand);
        if (profile is null) return BadRequest();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return CreatedAtAction(nameof(GetProfileById), new { profileId = profile.Id }, profileResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Profiles", "Get all profiles.", OperationId = "GetAllProfiles")]
    [SwaggerResponse(200, "The profiles were found and returned.", typeof(IEnumerable<ProfileResource>))]
    [SwaggerResponse(404, "The profiles were not found.")]
    public async Task<IActionResult> GetAllProfiles()
    {
        var getAllProfilesQuery = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(getAllProfilesQuery);
        var profileResources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(profileResources);
    }
    
    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update Profile",
        Description = "Update an existing profile by its ID.",
        OperationId = "UpdateProfile"
    )]
    [SwaggerResponse(200, "The profile was updated successfully.", typeof(ProfileResource))]
    [SwaggerResponse(400, "Invalid data sent to the API.")]
    [SwaggerResponse(404, "No profile found for the given ID.")]
    public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileResource resource)
    {
        if (resource == null)
            return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });
        
        if (string.IsNullOrWhiteSpace(resource.Name) || string.IsNullOrWhiteSpace(resource.Email))
            return BadRequest(new { message = "Nombre y correo electrónico son obligatorios." });

        try
        {
            var command = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var profile = await profileCommandService.Handle(command);

            if (profile == null)
                return NotFound(new { message = $"No existe un perfil con el ID {id}." });

            var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
            return Ok(profileResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar el perfil.", details = ex.Message });
        }
    }

}