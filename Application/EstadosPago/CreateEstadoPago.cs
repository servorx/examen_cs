using Domain.ValueObjects;
using MediatR;

namespace Application.EstadosPago;

public sealed record CreateEstadoPago(
    NombreVO Nombre
) : IRequest<IdVO>;
