using FluentValidation;

namespace Application.TipoServicios;

public class UpdateTipoServicioValidator : AbstractValidator<UpdateTipoServicio>
{
    public UpdateTipoServicioValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre del servicio es obligatorio.");
        RuleFor(x => x.Descripcion).NotNull().WithMessage("La descripción es obligatoria.");
        RuleFor(x => x.PrecioBase).NotNull().WithMessage("El precio base es obligatorio.");
    }
}
