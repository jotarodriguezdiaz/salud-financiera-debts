using MediatR;

namespace Debts.Application.Features.SimulatorDebts.Comammds.DeleteSimulation
{
    public class DeleteSimulationCommand : IRequest<bool>
    {                
        public int SimulationId { get; set; }
        public Guid UserId { get; set; }
    }
}
