using AutoMapper;
using Debts.Application.Contracts.Persistence;
using MediatR;

namespace Debts.Application.Features.SimulatorDebts.Queries.GetSimulations
{
    public class GetSimulationsQueryHandler : IRequestHandler<GetSimulationsQuery, List<GetSimulationsResult>>
    {
        private readonly ISimulationRepository _repository;
        private readonly IMapper _mapper;

        public GetSimulationsQueryHandler(ISimulationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetSimulationsResult>> Handle(GetSimulationsQuery request, CancellationToken cancellationToken)
        {
            var simulations = await _repository.GetAllAsync(request.UserId);

            return _mapper.Map<List<GetSimulationsResult>>(simulations);
        }
    }
}
