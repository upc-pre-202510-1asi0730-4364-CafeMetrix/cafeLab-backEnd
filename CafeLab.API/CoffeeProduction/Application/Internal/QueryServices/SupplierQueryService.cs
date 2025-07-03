using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Queries;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.CoffeeProduction.Domain.Services;

namespace CafeLab.API.CoffeeProduction.Application.Internal.QueryServices;

public class SupplierQueryService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    public async Task<IEnumerable<Supplier>> Handle(GetAllSuppliersByUserIdQuery query)
        => await supplierRepository.FindAllByUserIdAsync(query.UserId);

    public async Task<Supplier?> Handle(GetSupplierByIdQuery query)
        => await supplierRepository.FindByIdAndUserIdAsync(query.Id, query.UserId);

    public async Task<IEnumerable<Supplier>> Handle(SearchSuppliersByNameQuery query)
        => await supplierRepository.SearchByNameAndUserIdAsync(query.Name, query.UserId);
} 