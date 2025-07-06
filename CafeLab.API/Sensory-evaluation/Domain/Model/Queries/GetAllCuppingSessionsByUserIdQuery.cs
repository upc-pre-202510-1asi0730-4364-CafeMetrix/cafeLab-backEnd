namespace CafeLab.API.Sensory_evaluation.Domain.Model.Queries
{
    public class GetAllCuppingSessionsByUserIdQuery
    {
        public int UserId { get; }

        public GetAllCuppingSessionsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
} 