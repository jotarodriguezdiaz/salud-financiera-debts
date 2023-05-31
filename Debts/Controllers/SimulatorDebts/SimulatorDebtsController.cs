using Debts.API.Controllers;
using Debts.Application.Features.SimulatorDebts.Queries.Simultate;
using Debts.Application.Features.SimulatorDebts.Queries.Simultate.Model.Loan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Debts.Controllers.SimulatorDebts
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorDebtsController : BaseController
    {
        private readonly IMediator _mediator;

        public SimulatorDebtsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("simulate")]
        public async Task<ActionResult<IEnumerable<Loan>>> Simulate([FromQuery] decimal import,
                                                                    [FromQuery] decimal typeInterest,
                                                                    [FromQuery] int time,
                                                                    [FromQuery] int annualProfitability,
                                                                    [FromQuery] int taxes,
                                                                    [FromQuery] int monthlySaving)
        {
            var query = new SimulateQuery
            {
                UserId = GetUserIdFromToken(),
                Import = import,
                TypeInterest = typeInterest,
                Time = time,
                AnnualProfitability = annualProfitability,
                Taxes = taxes,
                MonthlySaving = monthlySaving
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
