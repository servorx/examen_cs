using FluentValidation;

namespace Application.Clientes;

public class UpdateClienteValidator : AbstractValidator<UpdateCliente>
{
    public UpdateClienteValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Nombre).NotNull();
        RuleFor(x => x.Correo).NotNull();
        RuleFor(x => x.Telefono).NotNull();
        RuleFor(x => x.Direccion).NotNull();
        RuleFor(x => x.IsActive).NotNull();
    }
}
