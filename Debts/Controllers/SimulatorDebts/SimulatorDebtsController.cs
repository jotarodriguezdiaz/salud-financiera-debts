using Debts.API.Controllers;
using Debts.Application.Features.SimulatorDebts.Comammds.CreateSimulation;
using Debts.Application.Features.SimulatorDebts.Comammds.DeleteSimulation;
using Debts.Application.Features.SimulatorDebts.Queries.GetSimulations;
using Debts.Application.Features.SimulatorDebts.Queries.Simultate;
using Debts.Application.Features.SimulatorDebts.Queries.Simultate.Model.Loan;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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


        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<GetSimulationsResult>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<GetSimulationsResult>>> GetUserBoards()
        {
            var query = new GetSimulationsQuery(GetUserIdFromToken());
            var result = await _mediator.Send(query);
            return Ok(result);
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

        [HttpPost]
        public async Task<ActionResult<int>> Add(CreateSimulationCommand command)
        {
            command.UserId = GetUserIdFromToken();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteSimulationCommand
            {
                UserId = GetUserIdFromToken(),
                SimulationId = id
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
