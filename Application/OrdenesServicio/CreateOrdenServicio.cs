using Domain.ValueObjects;
using MediatR;

namespace Application.OrdenesServicio;

public sealed record CreateOrdenServicio(
    IdVO VehiculoId,
    IdVO MecanicoId,
    IdVO TipoServicioId,
    IdVO RecepcionistaId,
    IdVO EstadoId,
    FechaHistoricaVO FechaIngreso,
    FechaHistoricaVO FechaEntregaEstimada
) : IRequest<IdVO>;