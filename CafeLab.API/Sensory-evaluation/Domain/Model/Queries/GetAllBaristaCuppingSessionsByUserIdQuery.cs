namespace CafeLab.API.Sensory_evaluation.Domain.Model.Queries
{
    public class GetAllBaristaCuppingSessionsByUserIdQuery
    {
        public int UserId { get; }

        public GetAllBaristaCuppingSessionsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
} 