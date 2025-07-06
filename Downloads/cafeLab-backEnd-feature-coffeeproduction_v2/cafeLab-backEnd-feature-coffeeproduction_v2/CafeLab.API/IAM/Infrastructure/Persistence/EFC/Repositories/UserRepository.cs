using CafeLab.API.IAM.Domain.Model.Aggregates;
using CafeLab.API.IAM.Domain.Model.ValueObjects;
using CafeLab.API.IAM.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.IAM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// User repository implementation
/// </summary>
public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(EmailAddress email)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.Email.Address == email.Address);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> ExistsByEmailAsync(EmailAddress email)
    {
        return await Context.Set<User>()
            .AnyAsync(u => u.Email.Address == email.Address);
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await Context.Set<User>()
            .AnyAsync(u => u.Username == username);
    }

    public async Task UpdateAsync(User entity)
    {
        base.Update(entity);
        await Task.CompletedTask;
    }
} 