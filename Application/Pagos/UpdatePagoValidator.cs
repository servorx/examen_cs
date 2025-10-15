using FluentValidation;

namespace Application.Pagos;

public class UpdatePagoValidator : AbstractValidator<UpdatePago>
{
    public UpdatePagoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.FacturaId).NotNull();
        RuleFor(x => x.MetodoPagoId).NotNull();
        RuleFor(x => x.EstadoPagoId).NotNull();
        RuleFor(x => x.Monto).NotNull();
        RuleFor(x => x.FechaPago).NotNull();
    }
}
