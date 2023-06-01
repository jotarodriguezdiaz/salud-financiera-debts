using AutoMapper;
using Debts.Application.Features.SimulatorDebts.Comammds.CreateSimulation;
using Debts.Application.Features.SimulatorDebts.Queries.GetSimulations;
using Debts.Domain;

namespace Debts.Application.Features.SimulatorDebts.Mappings
{
    public class SimulationMappingProfile : Profile
    {
        public SimulationMappingProfile()
        {
            // Commands
            CreateMap<CreateSimulationCommand, Simulation>();

            // Queries
            CreateMap<Simulation, GetSimulationsResult>();
        }
    }
}
