namespace CafeLab.API.Administration.Domain.Model.Queries
{
    public class GetAllCostosLoteByUserIdQuery
    {
        public int UserId { get; }

        public GetAllCostosLoteByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
} 