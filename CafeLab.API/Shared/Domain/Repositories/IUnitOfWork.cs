using System.Threading.Tasks;

namespace CafeLab.API.Shared.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
} 