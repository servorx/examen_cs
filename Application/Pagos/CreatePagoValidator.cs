using FluentValidation;

namespace Application.Pagos;

public class CreatePagoValidator : AbstractValidator<CreatePago>
{
    public CreatePagoValidator()
    {
        RuleFor(x => x.FacturaId).NotNull();
        RuleFor(x => x.MetodoPagoId).NotNull();
        RuleFor(x => x.EstadoPagoId).NotNull();
        RuleFor(x => x.Monto).NotNull();
        RuleFor(x => x.FechaPago).NotNull();
    }
}
