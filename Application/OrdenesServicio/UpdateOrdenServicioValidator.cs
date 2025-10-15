using FluentValidation;

namespace Application.OrdenesServicio;

public class UpdateOrdenServicioValidator : AbstractValidator<UpdateOrdenServicio>
{
    public UpdateOrdenServicioValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.VehiculoId).NotNull();
        RuleFor(x => x.MecanicoId).NotNull();
        RuleFor(x => x.TipoServicioId).NotNull();
        RuleFor(x => x.EstadoId).NotNull();
        RuleFor(x => x.FechaIngreso).NotNull();
        RuleFor(x => x.FechaEntregaEstimada).NotNull();
    }
}
