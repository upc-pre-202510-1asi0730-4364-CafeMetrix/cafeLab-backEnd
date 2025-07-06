using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.CoffeeProduction.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;
using CafeLab.API.Profiles.Domain.Repositories;

namespace CafeLab.API.CoffeeProduction.Application.Internal.CommandServices;

public class RoastProfileCommandService : IRoastProfileCommandService
{
    private readonly IRoastProfileRepository _roastProfileRepository;
    private readonly ICoffeeLotRepository _coffeeLotRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoastProfileCommandService(
        IRoastProfileRepository roastProfileRepository,
        ICoffeeLotRepository coffeeLotRepository,
        IProfileRepository profileRepository,
        IUnitOfWork unitOfWork)
    {
        _roastProfileRepository = roastProfileRepository;
        _coffeeLotRepository = coffeeLotRepository;
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RoastProfile> CreateAsync(CreateRoastProfileCommand command)
    {
        // Validar que el UserId exista en Profile
        var profile = await _profileRepository.FindByIdAsync(command.UserId);
        if (profile == null)
            throw new InvalidOperationException($"No existe un usuario registrado con id {command.UserId}");

        // Validar que el coffee lot exista y pertenezca al usuario
        var coffeeLot = await _coffeeLotRepository.GetByIdAsync(command.CoffeeLotId);
        if (coffeeLot == null)
            throw new InvalidOperationException("Coffee lot not found");
        if (coffeeLot.UserId != command.UserId)
            throw new InvalidOperationException("Coffee lot does not belong to user");

        var roastProfile = new RoastProfile
        {
            ProfileName = command.ProfileName,
            RoastType = command.RoastType,
            Duration = command.Duration,
            CoffeeLotId = command.CoffeeLotId,
            TempStart = command.TempStart,
            TempEnd = command.TempEnd,
            IsFavorite = command.IsFavorite,
            UserId = command.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _roastProfileRepository.AddAsync(roastProfile);
        await _unitOfWork.CompleteAsync();
        return roastProfile;
    }

    public async Task<RoastProfile> UpdateAsync(UpdateRoastProfileCommand command)
    {
        var roastProfile = await _roastProfileRepository.GetByIdAsync(command.Id);
        if (roastProfile == null)
            throw new InvalidOperationException("Roast profile not found");

        // Validar que el coffee lot exista
        var coffeeLot = await _coffeeLotRepository.GetByIdAsync(command.CoffeeLotId);
        if (coffeeLot == null)
            throw new InvalidOperationException("Coffee lot not found");

        roastProfile.ProfileName = command.ProfileName;
        roastProfile.RoastType = command.RoastType;
        roastProfile.Duration = command.Duration;
        roastProfile.CoffeeLotId = command.CoffeeLotId;
        roastProfile.TempStart = command.TempStart;
        roastProfile.TempEnd = command.TempEnd;
        roastProfile.IsFavorite = command.IsFavorite;
        roastProfile.UpdatedAt = DateTime.UtcNow;

        _roastProfileRepository.Update(roastProfile);
        await _unitOfWork.CompleteAsync();
        return roastProfile;
    }

    public async Task DeleteAsync(DeleteRoastProfileCommand command)
    {
        var roastProfile = await _roastProfileRepository.GetByIdAsync(command.Id);
        if (roastProfile == null)
            throw new InvalidOperationException("Roast profile not found");
        _roastProfileRepository.Remove(roastProfile);
        await _unitOfWork.CompleteAsync();
    }
} 