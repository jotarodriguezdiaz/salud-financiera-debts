using Debts.Application.Features.SimulatorDebts.Queries.Simultate.Model.Loan;
using MediatR;

namespace Debts.Application.Features.SimulatorDebts.Queries.Simultate
{
    public class SimulateQueryHandler : IRequestHandler<SimulateQuery, Loan>
    {
        public async Task<Loan> Handle(SimulateQuery request, CancellationToken cancellationToken)
        {
            return new Loan(request.Import, request.TypeInterest, request.Time, request.AnnualProfitability, request.Taxes, request.MonthlySaving);
        }
    }
}