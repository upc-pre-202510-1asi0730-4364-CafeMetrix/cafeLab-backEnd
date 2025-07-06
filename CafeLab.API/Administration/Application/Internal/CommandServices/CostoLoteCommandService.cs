using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Model.Commands;
using CafeLab.API.Administration.Domain.Repositories;
using CafeLab.API.Administration.Domain.Services;

namespace CafeLab.API.Administration.Application.Internal.CommandServices
{
    public class CostoLoteCommandService : ICostoLoteCommandService
    {
        private readonly ICostoLoteRepository _repository;
        public CostoLoteCommandService(ICostoLoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<CostoLote> Handle(CreateCostoLoteCommand command)
        {
            var costoLote = new CostoLote(
                command.Fecha,
                command.Lote,
                command.MateriaPrima,
                command.ManoObra,
                command.Transporte,
                command.Almacenamiento,
                command.Procesamiento,
                command.OtrosCostos,
                command.UserId
            );
            return await _repository.AddAsync(costoLote);
        }

        public async Task<CostoLote> Handle(UpdateCostoLoteCommand command)
        {
            var costoLote = await _repository.GetByIdAsync(command.Id, command.UserId);
            if (costoLote == null) return null;
            costoLote.Update(
                command.Fecha,
                command.Lote,
                command.MateriaPrima,
                command.ManoObra,
                command.Transporte,
                command.Almacenamiento,
                command.Procesamiento,
                command.OtrosCostos
            );
            return await _repository.UpdateAsync(costoLote);
        }

        public async Task Handle(DeleteCostoLoteCommand command)
        {
            await _repository.DeleteAsync(command.Id, command.UserId);
        }
    }
} 