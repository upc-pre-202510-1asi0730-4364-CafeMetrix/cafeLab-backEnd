namespace CafeLab.API.Administration.Domain.Model.Queries
{
    public class GetAllMovimientosInventarioByUserIdQuery
    {
        public int UserId { get; }

        public GetAllMovimientosInventarioByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
} 