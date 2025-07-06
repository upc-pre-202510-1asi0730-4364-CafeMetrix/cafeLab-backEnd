namespace CafeLab.API.Sensory_evaluation.Domain.Model.Commands
{
    public class DeleteCuppingSessionCommand
    {
        public int Id { get; }
        public int UserId { get; }

        public DeleteCuppingSessionCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
} 