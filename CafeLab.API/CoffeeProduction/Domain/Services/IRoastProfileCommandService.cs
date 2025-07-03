using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Commands;

namespace CafeLab.API.CoffeeProduction.Domain.Services;

public interface IRoastProfileCommandService
{
    Task<RoastProfile> CreateAsync(CreateRoastProfileCommand command);
    Task<RoastProfile> UpdateAsync(UpdateRoastProfileCommand command);
    Task DeleteAsync(DeleteRoastProfileCommand command);
} 