using CafeLab.API.Defects.Domain.Model.Aggregates;
using CafeLab.API.Defects.Domain.Model.Commands;

namespace CafeLab.API.Defects.Domain.Services;

/// <summary>
/// Defect command service interface
/// </summary>
public interface IDefectCommandService
{
    Task<Defect> CreateAsync(CreateDefectCommand command);
    Task<Defect> UpdateAsync(UpdateDefectCommand command);
    Task DeleteAsync(DeleteDefectCommand command);
} 