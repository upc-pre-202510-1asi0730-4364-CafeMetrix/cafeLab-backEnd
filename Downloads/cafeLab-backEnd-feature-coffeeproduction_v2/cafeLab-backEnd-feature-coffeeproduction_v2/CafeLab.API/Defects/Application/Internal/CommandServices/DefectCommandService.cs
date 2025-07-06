using CafeLab.API.Defects.Domain.Model.Aggregates;
using CafeLab.API.Defects.Domain.Model.Commands;
using CafeLab.API.Defects.Domain.Repositories;
using CafeLab.API.Defects.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.Defects.Application.Internal.CommandServices;

/// <summary>
/// Defect command service implementation
/// </summary>
public class DefectCommandService : IDefectCommandService
{
    private readonly IDefectRepository _defectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DefectCommandService(IDefectRepository defectRepository, IUnitOfWork unitOfWork)
    {
        _defectRepository = defectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Defect> CreateAsync(CreateDefectCommand command)
    {
        var defect = new Defect(command);
        await _defectRepository.AddAsync(defect);
        await _unitOfWork.CompleteAsync();
        return defect;
    }

    public async Task<Defect> UpdateAsync(UpdateDefectCommand command)
    {
        var defect = await _defectRepository.FindByIdAsync(command.Id);
        if (defect == null)
            throw new InvalidOperationException($"Defect with ID {command.Id} not found");

        defect.Update(command);
        await _defectRepository.UpdateAsync(defect);
        await _unitOfWork.CompleteAsync();
        return defect;
    }

    public async Task DeleteAsync(DeleteDefectCommand command)
    {
        var defect = await _defectRepository.FindByIdAsync(command.Id);
        if (defect == null)
            throw new InvalidOperationException($"Defect with ID {command.Id} not found");

        defect.Deactivate();
        await _defectRepository.UpdateAsync(defect);
        await _unitOfWork.CompleteAsync();
    }
} 