using Debts.Application.Contracts.Persistence;
using MediatR;

namespace Debts.Application.Features.SimulatorDebts.Comammds.DeleteSimulation
{
    public class DeleteSimulationCommandHandler : IRequestHandler<DeleteSimulationCommand, bool>
    {
        private readonly ISimulationRepository _repository;

        public DeleteSimulationCommandHandler(ISimulationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteSimulationCommand request, CancellationToken cancellationToken)
        {
            await Validation(request);

            var goal = await _repository.GetByIdAsync(request.SimulationId);

            await _repository.DeleteAsync(goal);

            return true;
        }

        private async Task Validation(DeleteSimulationCommand request)
        {
            bool belongsToUser = await _repository.IsSimulationOwnedToUserAsync(request.SimulationId, request.UserId);

            if (!belongsToUser) throw new UnauthorizedAccessException();
        }
    }
}
