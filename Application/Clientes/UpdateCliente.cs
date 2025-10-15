using Domain.ValueObjects;
using MediatR;

namespace Application.Clientes;

public sealed record UpdateCliente(
    IdVO Id,
    NombreVO Nombre,
    CorreoVO Correo,
    TelefonoVO Telefono,
    DireccionVO Direccion,
    EstadoVO IsActive
) : IRequest<bool>;

