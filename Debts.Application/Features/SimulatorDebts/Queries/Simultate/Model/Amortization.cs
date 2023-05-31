namespace Debts.Application.Features.SimulatorDebts.Queries.Simultate.Model
{
    public class Amortization
    {
        public int Year { get; set; }
        public decimal CapitalPending { get; set; }
        public decimal Interests { get; set; }
        public decimal QuantityAmortized { get; set; }
        public decimal Quota { get; set; }
        public decimal TypeInterest { get; set; }
    }
}
