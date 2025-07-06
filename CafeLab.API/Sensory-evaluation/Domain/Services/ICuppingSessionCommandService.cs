using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Model.Commands;

namespace CafeLab.API.Sensory_evaluation.Domain.Services
{
    public interface ICuppingSessionCommandService
    {
        Task<CuppingSession> Handle(CreateCuppingSessionCommand command);
        Task<CuppingSession> Handle(UpdateCuppingSessionCommand command);
        Task Handle(DeleteCuppingSessionCommand command);
    }
} 