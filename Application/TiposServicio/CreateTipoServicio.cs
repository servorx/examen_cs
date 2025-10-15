using Domain.ValueObjects;
using MediatR;

namespace Application.TipoServicios;

public sealed record CreateTipoServicio(
    NombreVO Nombre,
    DescripcionVO Descripcion,
    DineroVO PrecioBase
) : IRequest<IdVO>;
