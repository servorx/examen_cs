using Domain.ValueObjects;
using MediatR;

namespace Application.Proveedores;

public sealed record CreateProveedor(
    NombreVO Nombre,
    TelefonoVO? Telefono,
    CorreoVO? Correo,
    DireccionVO? Direccion,
    EstadoVO IsActive,
    int UserId
) : IRequest<IdVO>;
