using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.CoffeeProduction.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;
using CafeLab.API.Profiles.Domain.Repositories;

namespace CafeLab.API.CoffeeProduction.Application.Internal.CommandServices;

public class SupplierCommandService : ISupplierCommandService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SupplierCommandService(
        ISupplierRepository supplierRepository,
        IProfileRepository profileRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Supplier?> Handle(CreateSupplierCommand command)
    {
        // Validar que el UserId exista en Profile
        var profile = await _profileRepository.FindByIdAsync(command.UserId);
        if (profile == null)
            throw new InvalidOperationException($"No existe un usuario registrado con id {command.UserId}");
        var supplier = new Supplier(command.Name, command.Email, command.Phone, command.Location, command.Specialties, command.UserId);
        await _supplierRepository.AddAsync(supplier);
        await _unitOfWork.CompleteAsync();
        return supplier;
    }

    public async Task<Supplier?> Handle(UpdateSupplierCommand command)
    {
        var supplier = await _supplierRepository.FindByIdAndUserIdAsync(command.Id, command.UserId);
        if (supplier == null) return null;
        supplier.Update(command.Name, command.Email, command.Phone, command.Location, command.Specialties);
        await _unitOfWork.CompleteAsync();
        return supplier;
    }

    public async Task<bool> Handle(DeleteSupplierCommand command)
    {
        await _supplierRepository.DeleteByIdAndUserIdAsync(command.Id, command.UserId);
        await _unitOfWork.CompleteAsync();
        return true;
    }
} 