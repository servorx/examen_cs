using FluentValidation;

namespace Application.Vehiculos;

public class UpdateVehiculoValidator : AbstractValidator<UpdateVehiculo>
{
    public UpdateVehiculoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.ClienteId).NotNull().WithMessage("El Id del cliente es obligatorio.");
        RuleFor(x => x.Marca).NotNull().WithMessage("La marca del vehículo es obligatoria.");
        RuleFor(x => x.Modelo).NotNull().WithMessage("El modelo del vehículo es obligatorio.");
        RuleFor(x => x.Anio).NotNull().WithMessage("El año del vehículo es obligatorio.");
        RuleFor(x => x.Vin).NotNull().WithMessage("El VIN del vehículo es obligatorio.");
        RuleFor(x => x.Kilometraje).NotNull().WithMessage("El kilometraje del vehículo es obligatorio.");
    }
}
