using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Model.Commands;

namespace CafeLab.API.Administration.Domain.Services
{
    public interface ICostoLoteCommandService
    {
        Task<CostoLote> Handle(CreateCostoLoteCommand command);
        Task<CostoLote> Handle(UpdateCostoLoteCommand command);
        Task Handle(DeleteCostoLoteCommand command);
    }
} 