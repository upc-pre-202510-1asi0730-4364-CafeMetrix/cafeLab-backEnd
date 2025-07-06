namespace CafeLab.API.Sensory_evaluation.Domain.Model.Queries
{
    public class GetBaristaCuppingSessionByIdQuery
    {
        public int Id { get; }
        public int UserId { get; }

        public GetBaristaCuppingSessionByIdQuery(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
} 