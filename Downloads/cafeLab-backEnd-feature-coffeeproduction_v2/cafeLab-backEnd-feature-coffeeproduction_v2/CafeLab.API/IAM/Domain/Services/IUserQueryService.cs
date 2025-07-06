using CafeLab.API.IAM.Domain.Model.Aggregates;
using CafeLab.API.IAM.Domain.Model.Queries;

namespace CafeLab.API.IAM.Domain.Services;

/// <summary>
/// User query service interface
/// </summary>
public interface IUserQueryService
{
    Task<User?> GetByIdAsync(GetUserByIdQuery query);
    Task<User?> GetByEmailAsync(GetUserByEmailQuery query);
    Task<IEnumerable<User>> GetAllAsync(GetAllUsersQuery query);
} 