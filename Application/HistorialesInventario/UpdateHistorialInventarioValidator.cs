using FluentValidation;

namespace Application.HistorialesInventario;

public class UpdateHistorialInventarioValidator : AbstractValidator<UpdateHistorialInventario>
{
    public UpdateHistorialInventarioValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("El Id del historial es obligatorio");
        RuleFor(x => x.RepuestoId).NotNull().WithMessage("El Id del repuesto es obligatorio");
        RuleFor(x => x.TipoMovimientoId).NotNull().WithMessage("El Id del tipo de movimiento es obligatorio");
        RuleFor(x => x.Cantidad).NotNull().WithMessage("La cantidad es obligatoria");
        RuleFor(x => x.FechaMovimiento).NotNull().WithMessage("La fecha de movimiento es obligatoria");
    }
}
