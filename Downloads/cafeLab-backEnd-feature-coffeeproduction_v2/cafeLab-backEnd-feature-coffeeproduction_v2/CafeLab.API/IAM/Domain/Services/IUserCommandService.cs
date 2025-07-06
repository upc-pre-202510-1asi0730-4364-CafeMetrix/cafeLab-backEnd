using CafeLab.API.IAM.Domain.Model.Aggregates;
using CafeLab.API.IAM.Domain.Model.Commands;

namespace CafeLab.API.IAM.Domain.Services;

/// <summary>
/// User command service interface
/// </summary>
public interface IUserCommandService
{
    Task<User> CreateAsync(CreateUserCommand command);
    Task<string> SignInAsync(SignInCommand command);
} 