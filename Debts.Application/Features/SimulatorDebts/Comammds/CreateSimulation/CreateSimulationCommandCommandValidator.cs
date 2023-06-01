using FluentValidation;

namespace Debts.Application.Features.SimulatorDebts.Comammds.CreateSimulation
{
    public class CreateSimulationCommandValidator : AbstractValidator<CreateSimulationCommand>
    {
        public CreateSimulationCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty().WithMessage("El ID del usuario es obligatorio.");
            RuleFor(command => command.Import).GreaterThan(0).WithMessage("El importe debe ser mayor que cero.");
            RuleFor(command => command.TypeInterest).GreaterThan(0).WithMessage("El tipo de interés debe ser mayor que cero.");
            RuleFor(command => command.Time).GreaterThan(0).WithMessage("El tiempo debe ser mayor que cero.");

            RuleFor(command => command.AnnualProfitability).GreaterThan(0).WithMessage("La rentabilidad anual debe ser mayor que cero.");
            RuleFor(command => command.Taxes).GreaterThanOrEqualTo(0).WithMessage("Los impuestos no pueden ser negativos.");
            RuleFor(command => command.MonthlySaving).GreaterThanOrEqualTo(0).WithMessage("El ahorro mensual no puede ser negativo.");
        }
    }
}
