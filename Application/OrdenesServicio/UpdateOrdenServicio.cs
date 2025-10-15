using Domain.ValueObjects;
using MediatR;

namespace Application.OrdenesServicio;

public sealed record UpdateOrdenServicio(
    IdVO Id,
    IdVO VehiculoId,
    IdVO MecanicoId,
    IdVO TipoServicioId,
    IdVO EstadoId,
    FechaHistoricaVO FechaIngreso,
    FechaHistoricaVO FechaEntregaEstimada
) : IRequest<bool>;
