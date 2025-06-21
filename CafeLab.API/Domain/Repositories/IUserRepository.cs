using CafeLab.API.Domain.Model.Aggregates;

namespace CafeLab.API.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByUsernameAsync(string username);
    }
} 