using Domain.ValueObjects;
using MediatR;

namespace Application.TipoMovimientos;

public sealed record CreateTipoMovimiento(
    NombreVO Nombre
) : IRequest<IdVO>;
