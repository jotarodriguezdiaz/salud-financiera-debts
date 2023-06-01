using Debts.Domain;

namespace Debts.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User?> FindUserByIdAsync(Guid userId);
    }
}
