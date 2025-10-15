
using FluentValidation;

namespace Application.EstadosCita;

public class UpdateEstadoCitaValidator : AbstractValidator<UpdateEstadoCita>
{
    public UpdateEstadoCitaValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("El Id es obligatorio");
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre no puede ser nulo");
    }
}
