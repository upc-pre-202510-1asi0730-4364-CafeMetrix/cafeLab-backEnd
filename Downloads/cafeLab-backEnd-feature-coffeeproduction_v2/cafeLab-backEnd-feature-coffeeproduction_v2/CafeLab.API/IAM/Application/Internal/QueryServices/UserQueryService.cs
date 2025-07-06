using CafeLab.API.IAM.Domain.Model.Aggregates;
using CafeLab.API.IAM.Domain.Model.Queries;
using CafeLab.API.IAM.Domain.Repositories;
using CafeLab.API.IAM.Domain.Services;

namespace CafeLab.API.IAM.Application.Internal.QueryServices;

/// <summary>
/// User query service implementation
/// </summary>
public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _userRepository;

    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByIdAsync(GetUserByIdQuery query)
    {
        return await _userRepository.FindByIdAsync(query.Id);
    }

    public async Task<User?> GetByEmailAsync(GetUserByEmailQuery query)
    {
        return await _userRepository.GetByEmailAsync(query.Email);
    }

    public async Task<IEnumerable<User>> GetAllAsync(GetAllUsersQuery query)
    {
        return await _userRepository.ListAsync();
    }
} 