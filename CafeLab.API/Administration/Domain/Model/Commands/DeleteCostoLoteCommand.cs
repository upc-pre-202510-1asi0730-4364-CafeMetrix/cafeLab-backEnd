namespace CafeLab.API.Administration.Domain.Model.Commands
{
    public class DeleteCostoLoteCommand
    {
        public int Id { get; }
        public int UserId { get; }

        public DeleteCostoLoteCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
} 