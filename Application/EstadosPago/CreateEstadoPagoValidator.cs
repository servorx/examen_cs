using FluentValidation;

namespace Application.EstadosPago;

public class CreateEstadoPagoValidator : AbstractValidator<CreateEstadoPago>
{
    public CreateEstadoPagoValidator()
    {
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre no puede ser nulo");
    }
}
