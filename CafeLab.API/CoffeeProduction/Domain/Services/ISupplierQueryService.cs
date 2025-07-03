using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Queries;

namespace CafeLab.API.CoffeeProduction.Domain.Services;

public interface ISupplierQueryService
{
    Task<IEnumerable<Supplier>> Handle(GetAllSuppliersByUserIdQuery query);
    Task<Supplier?> Handle(GetSupplierByIdQuery query);
    Task<IEnumerable<Supplier>> Handle(SearchSuppliersByNameQuery query);
} 