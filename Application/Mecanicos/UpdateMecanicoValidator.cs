using FluentValidation;

namespace Application.Mecanicos;

public class UpdateMecanicoValidator : AbstractValidator<UpdateMecanico>
{
    public UpdateMecanicoValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("El Id del mecánico es obligatorio");
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre es obligatorio");
        RuleFor(x => x.IsActive).NotNull().WithMessage("El estado activo es obligatorio");
    }
}
