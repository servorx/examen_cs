using Domain.ValueObjects;
using MediatR;

namespace Application.Repuestos;

public sealed record UpdateRepuesto(
    IdVO Id,
    CodigoRepuestoVO Codigo,
    DescripcionVO Descripcion,
    CantidadVO CantidadStock,
    DineroVO PrecioUnitario,
    IdVO? ProveedorId
) : IRequest<bool>;
