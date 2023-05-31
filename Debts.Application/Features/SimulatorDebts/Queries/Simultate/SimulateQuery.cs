using Debts.Application.Features.SimulatorDebts.Queries.Simultate.Model.Loan;
using MediatR;

namespace Debts.Application.Features.SimulatorDebts.Queries.Simultate
{
    public class SimulateQuery : IRequest<Loan>
    {
        public Guid UserId { get; set; }
        public decimal Import { get; set; }
        public decimal TypeInterest { get; set; }
        public int Time { get; set; }
        public int AnnualProfitability { get; set; }
        public int Taxes { get; set; }
        public int MonthlySaving { get; set; }
    }
}
