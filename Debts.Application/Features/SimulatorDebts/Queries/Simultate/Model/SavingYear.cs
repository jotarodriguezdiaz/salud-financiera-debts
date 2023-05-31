namespace Debts.Application.Features.SimulatorDebts.Queries.Simultate.Model
{
    public class SavingYear
    {
        public int Year { get; set; }
        public decimal AnnualSaving { get; set; }
        public decimal Interests { get; set; }
        public decimal AccumulatedSaving { get; set; }
        public decimal ValueAfterTaxes { get; set; }
        public decimal NetProfit { get; set; }
    }
}
