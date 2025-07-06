using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Commands;

namespace CafeLab.API.CoffeeProduction.Domain.Services;

public interface ICoffeeLotCommandService
{
    Task<CoffeeLot> CreateAsync(CreateCoffeeLotCommand command);
    Task<CoffeeLot> UpdateAsync(UpdateCoffeeLotCommand command);
    Task DeleteAsync(DeleteCoffeeLotCommand command);
} 