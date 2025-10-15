using Domain.ValueObjects;
using MediatR;

namespace Application.TipoServicios;

public sealed record UpdateTipoServicio(
    IdVO Id,
    NombreVO Nombre,
    DescripcionVO Descripcion,
    DineroVO PrecioBase
) : IRequest<bool>;
