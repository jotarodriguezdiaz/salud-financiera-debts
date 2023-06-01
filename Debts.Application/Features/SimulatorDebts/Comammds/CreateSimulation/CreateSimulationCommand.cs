using MediatR;

namespace Debts.Application.Features.SimulatorDebts.Comammds.CreateSimulation
{
    public class CreateSimulationCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }

        public decimal Import { get; set; }
        public decimal TypeInterest { get; set; }
        public int Time { get; set; }

        public decimal AnnualProfitability { get; set; }
        public decimal Taxes { get; set; }
        public decimal MonthlySaving { get; set; }
    }
}
