using FluentValidation;

namespace Application.Citas;

public class CreateCitaValidator : AbstractValidator<CreateCita>
{
    public CreateCitaValidator()
    {
        RuleFor(x => x.ClienteId).NotNull();
        RuleFor(x => x.VehiculoId).NotNull();
        RuleFor(x => x.FechaCita).NotNull();
        RuleFor(x => x.EstadoId).NotNull();
        RuleFor(x => x.Motivo)
        // estos metodos son para validar que la descripción no esté vacía y no exceda los 255 caracteres, no se por que no se puede con MaxLength
            .Must(m => m != null && m.Value != null && m.Value.Length <= 255)
            .WithMessage("Motivo must be at most 255 characters.");
    }
}
