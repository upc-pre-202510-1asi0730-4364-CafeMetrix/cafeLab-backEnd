namespace CafeLab.API.Sensory_evaluation.Domain.Model.Commands
{
    public class DeleteBaristaCuppingSessionCommand
    {
        public int Id { get; }
        public int UserId { get; }

        public DeleteBaristaCuppingSessionCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
} 