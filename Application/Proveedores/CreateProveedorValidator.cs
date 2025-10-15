using FluentValidation;

namespace Application.Proveedores;

public class CreateProveedorValidator : AbstractValidator<CreateProveedor>
{
    public CreateProveedorValidator()
    {
        RuleFor(x => x.Nombre).NotNull();
        RuleFor(x => x.IsActive).NotNull();
        // Telefono, Correo y Direccion son opcionales por lo que no se validan si son nulos
    }
}
