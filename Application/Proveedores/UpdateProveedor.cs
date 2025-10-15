using Domain.ValueObjects;
using MediatR;

namespace Application.Proveedores;

public sealed record UpdateProveedor(
    IdVO Id,
    NombreVO Nombre,
    TelefonoVO? Telefono,
    CorreoVO? Correo,
    DireccionVO? Direccion,
    EstadoVO IsActive
) : IRequest<bool>;
