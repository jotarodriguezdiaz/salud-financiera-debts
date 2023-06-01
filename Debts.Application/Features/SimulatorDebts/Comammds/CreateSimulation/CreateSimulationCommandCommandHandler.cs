using AutoMapper;
using Debts.Application.Contracts.Persistence;
using Debts.Domain;
using MediatR;

namespace Debts.Application.Features.SimulatorDebts.Comammds.CreateSimulation
{
    public class CreateSimulationCommandCommandHandler : IRequestHandler<CreateSimulationCommand, bool>
    {
        private readonly ISimulationRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateSimulationCommandCommandHandler(ISimulationRepository repository, IUserRepository userRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateSimulationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindUserByIdAsync(request.UserId);
            if (user == null)
            {
                user = new User { UserId = request.UserId };
                await _userRepository.AddAsync(user);
            }

            var simulation = _mapper.Map<Simulation>(request);
            simulation.UserId = request.UserId;

            await _repository.AddAsync(simulation);

            return true;
        }
    }
}
