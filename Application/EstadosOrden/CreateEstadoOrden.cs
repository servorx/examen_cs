
using Domain.ValueObjects;
using MediatR;

namespace Application.EstadosOrden;

public sealed record CreateEstadoOrden(
    NombreVO Nombre
) : IRequest<IdVO>;
