namespace Debts.Application.Features.SimulatorDebts.Queries.GetSimulations
{
    public class GetSimulationsResult
    {
        public int SimulationId { get; set; }

        public decimal Import { get; set; }
        public decimal TypeInterest { get; set; }
        public int Time { get; set; }

        public decimal AnnualProfitability { get; set; }
        public decimal Taxes { get; set; }
        public decimal MonthlySaving { get; set; }
    }
}
