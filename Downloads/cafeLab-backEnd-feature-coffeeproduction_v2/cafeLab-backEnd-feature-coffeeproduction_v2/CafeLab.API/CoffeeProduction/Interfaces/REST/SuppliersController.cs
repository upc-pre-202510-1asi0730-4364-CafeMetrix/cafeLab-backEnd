using System.Net.Mime;
using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Domain.Model.Queries;
using CafeLab.API.CoffeeProduction.Domain.Services;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CafeLab.API.CoffeeProduction.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Supplier Endpoints.")]
public class SuppliersController(ISupplierCommandService supplierCommandService, ISupplierQueryService supplierQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Suppliers by User", "Get all suppliers for a user.", OperationId = "GetAllSuppliersByUserId")]
    public async Task<IActionResult> GetAllSuppliers([FromQuery] int userId)
    {
        var query = new GetAllSuppliersByUserIdQuery(userId);
        var suppliers = await supplierQueryService.Handle(query);
        var resources = suppliers.Select(SupplierResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("search")]
    [SwaggerOperation("Search Suppliers by Name", "Search suppliers by name for a user.", OperationId = "SearchSuppliersByName")]
    public async Task<IActionResult> SearchSuppliers([FromQuery] string name, [FromQuery] int userId)
    {
        var query = new SearchSuppliersByNameQuery(name, userId);
        var suppliers = await supplierQueryService.Handle(query);
        var resources = suppliers.Select(SupplierResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Supplier by Id", "Get a supplier by its id and user.", OperationId = "GetSupplierById")]
    public async Task<IActionResult> GetSupplierById(int id, [FromQuery] int userId)
    {
        var query = new GetSupplierByIdQuery(id, userId);
        var supplier = await supplierQueryService.Handle(query);
        if (supplier is null) return NotFound();
        var resource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation("Create Supplier", "Create a new supplier.", OperationId = "CreateSupplier")]
    public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierResource resource)
    {
        var command = CreateSupplierCommandFromResourceAssembler.ToCommandFromResource(resource);
        var supplier = await supplierCommandService.Handle(command);
        if (supplier is null) return BadRequest();
        var supplierResource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);
        return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.Id, userId = supplier.UserId }, supplierResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Update Supplier", "Update an existing supplier.", OperationId = "UpdateSupplier")]
    public async Task<IActionResult> UpdateSupplier(int id, [FromBody] CreateSupplierResource resource)
    {
        var command = new UpdateSupplierCommand(id, resource.Name, resource.Email, resource.Phone, resource.Location, resource.Specialties, resource.UserId);
        var supplier = await supplierCommandService.Handle(command);
        if (supplier is null) return NotFound();
        var supplierResource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);
        return Ok(supplierResource);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation("Delete Supplier", "Delete a supplier by id and user.", OperationId = "DeleteSupplier")]
    public async Task<IActionResult> DeleteSupplier(int id, [FromQuery] int userId)
    {
        var command = new DeleteSupplierCommand(id, userId);
        var result = await supplierCommandService.Handle(command);
        if (!result) return NotFound();
        return NoContent();
    }
} 