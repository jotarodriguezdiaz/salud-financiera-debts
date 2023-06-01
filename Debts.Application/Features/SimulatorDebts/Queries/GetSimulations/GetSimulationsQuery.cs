using MediatR;

namespace Debts.Application.Features.SimulatorDebts.Queries.GetSimulations
{
    public class GetSimulationsQuery : IRequest<List<GetSimulationsResult>>
    {
        public GetSimulationsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
