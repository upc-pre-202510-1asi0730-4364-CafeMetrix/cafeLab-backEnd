namespace CafeLab.API.Administration.Domain.Model.Queries
{
    public class GetMovimientoInventarioByIdQuery
    {
        public int Id { get; }
        public int UserId { get; }

        public GetMovimientoInventarioByIdQuery(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
} 