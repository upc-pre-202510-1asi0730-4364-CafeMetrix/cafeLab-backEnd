namespace CafeLab.API.Administration.Domain.Model.Queries
{
    public class GetCostoLoteByIdQuery
    {
        public int Id { get; }
        public int UserId { get; }

        public GetCostoLoteByIdQuery(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
} 