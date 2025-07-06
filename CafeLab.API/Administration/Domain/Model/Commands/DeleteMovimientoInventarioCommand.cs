namespace CafeLab.API.Administration.Domain.Model.Commands
{
    public class DeleteMovimientoInventarioCommand
    {
        public int Id { get; }
        public int UserId { get; }

        public DeleteMovimientoInventarioCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
} 