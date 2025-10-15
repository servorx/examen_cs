using FluentValidation;

namespace Application.EstadosOrden;

public class UpdateEstadoOrdenValidator : AbstractValidator<UpdateEstadoOrden>
{
    public UpdateEstadoOrdenValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("El Id es obligatorio");
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre no puede ser nulo");
    }
}
