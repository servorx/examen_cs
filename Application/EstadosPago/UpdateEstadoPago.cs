using Domain.ValueObjects;
using MediatR;

namespace Application.EstadosPago;

public sealed record UpdateEstadoPago(
    IdVO Id,
    NombreVO Nombre
) : IRequest<bool>;
