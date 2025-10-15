using FluentValidation;

namespace Application.EstadosOrden;

public class CreateEstadoOrdenValidator : AbstractValidator<CreateEstadoOrden>
{
    public CreateEstadoOrdenValidator()
    {
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre no puede ser nulo");
    }
}
