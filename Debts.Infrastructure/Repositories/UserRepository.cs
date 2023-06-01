using Debts.Application.Contracts.Persistence;
using Debts.Domain;
using Debts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Debts.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DebtsContext context) : base(context)
        {
        }

        public async Task<User?> FindUserByIdAsync(Guid userId)
        {
            return await _context.Users.SingleOrDefaultAsync(i => i.UserId == userId);
        }
    }
}
