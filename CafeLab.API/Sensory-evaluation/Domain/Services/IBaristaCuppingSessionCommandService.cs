using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Model.Commands;

namespace CafeLab.API.Sensory_evaluation.Domain.Services
{
    public interface IBaristaCuppingSessionCommandService
    {
        Task<BaristaCuppingSession> Handle(CreateBaristaCuppingSessionCommand command);
        Task<BaristaCuppingSession> Handle(UpdateBaristaCuppingSessionCommand command);
        Task Handle(DeleteBaristaCuppingSessionCommand command);
    }
} 