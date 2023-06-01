using Debts.Application.Contracts.Persistence;
using Debts.Domain;
using Debts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Debts.Infrastructure.Repositories
{
    public class SimulationRepository : RepositoryBase<Simulation>, ISimulationRepository
    {
        public SimulationRepository(DebtsContext context) : base(context)
        {
        }

        public async Task<ICollection<Simulation>> GetAllAsync(Guid userId)
        {
            return await _context.Simulations
              .Where(i => i.UserId == userId)
              .ToListAsync();
        }

        public async Task<bool> IsSimulationOwnedToUserAsync(int simulationId, Guid userId)
        {
            return await _context.Simulations.AnyAsync(i => i.SimulationId == simulationId && i.UserId == userId);
        }
    }
}