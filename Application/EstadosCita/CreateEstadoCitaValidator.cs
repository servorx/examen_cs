
using FluentValidation;

namespace Application.EstadosCita;

public class CreateEstadoCitaValidator : AbstractValidator<CreateEstadoCita>
{
    public CreateEstadoCitaValidator()
    {
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre no puede ser nulo");
    }
}
