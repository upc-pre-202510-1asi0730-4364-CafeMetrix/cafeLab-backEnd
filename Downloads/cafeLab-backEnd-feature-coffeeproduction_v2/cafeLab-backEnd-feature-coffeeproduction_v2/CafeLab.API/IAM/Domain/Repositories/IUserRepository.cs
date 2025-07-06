using CafeLab.API.IAM.Domain.Model.Aggregates;
using CafeLab.API.IAM.Domain.Model.ValueObjects;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.IAM.Domain.Repositories;

/// <summary>
/// User repository interface
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(EmailAddress email);
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(EmailAddress email);
    Task<bool> ExistsByUsernameAsync(string username);
    Task UpdateAsync(User entity);
} 