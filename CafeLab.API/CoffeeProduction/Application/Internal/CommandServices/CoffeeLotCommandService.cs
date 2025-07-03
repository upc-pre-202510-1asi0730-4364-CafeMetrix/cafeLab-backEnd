using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.CoffeeProduction.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;
using System.Text.Json;

namespace CafeLab.API.CoffeeProduction.Application.Internal.CommandServices;

public class CoffeeLotCommandService : ICoffeeLotCommandService
{
    private readonly ICoffeeLotRepository _coffeeLotRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CoffeeLotCommandService(
        ICoffeeLotRepository coffeeLotRepository,
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _coffeeLotRepository = coffeeLotRepository;
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CoffeeLot> CreateAsync(CreateCoffeeLotCommand command)
    {
        // Validate supplier exists and belongs to user
        var supplier = await _supplierRepository.GetByIdAsync(command.SupplierId);
        if (supplier == null)
            throw new InvalidOperationException("Supplier not found");
        
        if (supplier.UserId != command.UserId)
            throw new InvalidOperationException("Supplier does not belong to user");

        var coffeeLot = new CoffeeLot
        {
            LotName = command.LotName,
            CoffeeType = command.CoffeeType,
            ProcessingMethod = command.ProcessingMethod,
            Altitude = command.Altitude,
            Weight = command.Weight,
            Certifications = JsonSerializer.Serialize(command.Certifications),
            Origin = command.Origin,
            SupplierId = command.SupplierId,
            UserId = command.UserId,
            Status = command.Status
        };

        await _coffeeLotRepository.AddAsync(coffeeLot);
        await _unitOfWork.CompleteAsync();

        return coffeeLot;
    }

    public async Task<CoffeeLot> UpdateAsync(UpdateCoffeeLotCommand command)
    {
        var coffeeLot = await _coffeeLotRepository.GetByIdAsync(command.Id);
        if (coffeeLot == null)
            throw new InvalidOperationException("Coffee lot not found");

        // Validate supplier exists
        var supplier = await _supplierRepository.GetByIdAsync(command.SupplierId);
        if (supplier == null)
            throw new InvalidOperationException("Supplier not found");

        coffeeLot.LotName = command.LotName;
        coffeeLot.CoffeeType = command.CoffeeType;
        coffeeLot.ProcessingMethod = command.ProcessingMethod;
        coffeeLot.Altitude = command.Altitude;
        coffeeLot.Weight = command.Weight;
        coffeeLot.Certifications = JsonSerializer.Serialize(command.Certifications);
        coffeeLot.Origin = command.Origin;
        coffeeLot.SupplierId = command.SupplierId;
        coffeeLot.Status = command.Status;

        _coffeeLotRepository.Update(coffeeLot);
        await _unitOfWork.CompleteAsync();

        return coffeeLot;
    }

    public async Task DeleteAsync(DeleteCoffeeLotCommand command)
    {
        var coffeeLot = await _coffeeLotRepository.GetByIdAsync(command.Id);
        if (coffeeLot == null)
            throw new InvalidOperationException("Coffee lot not found");

        _coffeeLotRepository.Remove(coffeeLot);
        await _unitOfWork.CompleteAsync();
    }
} 