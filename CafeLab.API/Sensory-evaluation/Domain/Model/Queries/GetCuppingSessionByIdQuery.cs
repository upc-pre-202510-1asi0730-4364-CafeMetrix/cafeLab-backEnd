namespace CafeLab.API.Sensory_evaluation.Domain.Model.Queries
{
    public class GetCuppingSessionByIdQuery
    {
        public int Id { get; }
        public int UserId { get; }

        public GetCuppingSessionByIdQuery(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
} 