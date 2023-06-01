using Debts.Domain;

namespace Debts.Application.Contracts.Persistence
{
    public interface ISimulationRepository : IAsyncRepository<Simulation>
    {
        Task<ICollection<Simulation>> GetAllAsync(Guid userId);
        Task<bool> IsSimulationOwnedToUserAsync(int goalId, Guid userId);
    }
}