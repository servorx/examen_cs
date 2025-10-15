
using Domain.ValueObjects;
using MediatR;

namespace Application.Clientes;

public sealed record CreateCliente(
    NombreVO Nombre,
    CorreoVO Correo,
    TelefonoVO Telefono,
    DireccionVO Direccion,
    EstadoVO IsActive,
    int UserId
) : IRequest<IdVO>;
