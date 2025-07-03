using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.CoffeeProduction.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.CoffeeProduction.Application.Internal.CommandServices;

public class SupplierCommandService(
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork) : ISupplierCommandService
{
    public async Task<Supplier?> Handle(CreateSupplierCommand command)
    {
        var supplier = new Supplier(command.Name, command.Email, command.Phone, command.Location, command.Specialties, command.UserId);
        await supplierRepository.AddAsync(supplier);
        await unitOfWork.CompleteAsync();
        return supplier;
    }

    public async Task<Supplier?> Handle(UpdateSupplierCommand command)
    {
        var supplier = await supplierRepository.FindByIdAndUserIdAsync(command.Id, command.UserId);
        if (supplier == null) return null;
        supplier.Update(command.Name, command.Email, command.Phone, command.Location, command.Specialties);
        await unitOfWork.CompleteAsync();
        return supplier;
    }

    public async Task<bool> Handle(DeleteSupplierCommand command)
    {
        await supplierRepository.DeleteByIdAndUserIdAsync(command.Id, command.UserId);
        await unitOfWork.CompleteAsync();
        return true;
    }
} 