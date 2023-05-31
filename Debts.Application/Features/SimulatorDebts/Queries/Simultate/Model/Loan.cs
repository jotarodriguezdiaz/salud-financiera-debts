namespace Debts.Application.Features.SimulatorDebts.Queries.Simultate.Model.Loan
{
    public class Loan
    {
        public decimal Import { get; set; }
        public decimal TypeInterest { get; set; }
        public int Time { get; set; }

        public decimal AnnualProfitability { get; set; }
        public decimal Taxes { get; set; }
        public decimal MonthlySaving { get; set; }

        public List<Amortization> TableAmortization { get; set; }
        public List<SavingYear> TableSavings { get; set; }


        public Loan(decimal import, decimal typeInterest, int time, decimal annualProfitability, decimal taxes, decimal monthlySaving)
        {
            Import = import;
            TypeInterest = typeInterest;
            Time = time;

            AnnualProfitability = annualProfitability;
            Taxes = taxes;
            MonthlySaving = monthlySaving;

            TableAmortization = new List<Amortization>();
            TableSavings = new List<SavingYear>();

            CalculateAmortization();
            CalculateSavings();
        }

        private void CalculateAmortization()
        {
            decimal r = TypeInterest / 100 / 12; // Tasa de interés mensual
            int n = Time * 12; // Número total de pagos
            decimal quota = (r * Import) / (1 - (decimal)Math.Pow((double)(1 + r), -n));
            decimal capitalPending = Import;

            for (int i = 0; i < Time; i++)
            {
                decimal interests = 0;
                decimal quantityAmortized = 0;

                for (int j = 0; j < 12; j++)
                {
                    interests += capitalPending * r;
                    quantityAmortized += quota - capitalPending * r;
                    capitalPending -= quota - capitalPending * r;
                }

                TableAmortization.Add(new Amortization
                {
                    Year = i + 1,
                    CapitalPending = capitalPending,
                    Interests = interests,
                    QuantityAmortized = quantityAmortized,
                    Quota = quota * 12,
                    TypeInterest = TypeInterest
                });
            }
        }

        private void CalculateSavings()
        {
            decimal accumulatedSaving = 0;

            for (int i = 1; i <= Time; i++)
            {
                decimal annualSaving = MonthlySaving * 12;
                accumulatedSaving += annualSaving;

                decimal interests = (accumulatedSaving * AnnualProfitability) / 100;
                decimal totalBeforeTaxes = accumulatedSaving + interests;
                decimal taxesAmount = (totalBeforeTaxes * Taxes) / 100;
                decimal totalAfterTaxes = totalBeforeTaxes - taxesAmount;

                decimal netProfit = totalAfterTaxes - accumulatedSaving;

                TableSavings.Add(new SavingYear
                {
                    Year = i,
                    AnnualSaving = annualSaving,
                    Interests = interests,
                    AccumulatedSaving = accumulatedSaving,
                    ValueAfterTaxes = totalAfterTaxes,
                    NetProfit = netProfit
                });
            }
        }
    }
}
