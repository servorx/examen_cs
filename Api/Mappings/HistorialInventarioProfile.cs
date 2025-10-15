using Api.DTOs.HistorialesInventario;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class HistorialInventarioProfile : Profile
{
    public HistorialInventarioProfile()
    {
        // Entidad -> DTO simple
        CreateMap<HistorialInventario, HistorialInventarioDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.RepuestoId, opt => opt.MapFrom(src => src.RepuestoId.Value))
            .ForMember(dest => dest.AdminId, opt => opt.MapFrom(src => src.AdminId != null ? src.AdminId.Value : (int?)null))
            .ForMember(dest => dest.TipoMovimientoId, opt => opt.MapFrom(src => src.TipoMovimientoId.Value))
            .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad.Value))
            .ForMember(dest => dest.FechaMovimiento, opt => opt.MapFrom(src => src.FechaMovimiento.Value))
            .ForMember(dest => dest.Observaciones, opt => opt.MapFrom(src => src.Observaciones != null ? src.Observaciones.Value : null));

        // Crear DTO -> Entidad
        CreateMap<CreateHistorialInventarioDto, HistorialInventario>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // generado automáticamente
                dest.RepuestoId = new IdVO(src.RepuestoId);
                dest.AdminId = src.AdminId != null ? new IdVO(src.AdminId.Value) : null;
                dest.TipoMovimientoId = new IdVO(src.TipoMovimientoId);
                dest.Cantidad = new CantidadVO(src.Cantidad);
                dest.FechaMovimiento = new FechaHistoricaVO(src.FechaMovimiento);
                dest.Observaciones = src.Observaciones != null ? new DescripcionVO(src.Observaciones) : null;
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateHistorialInventarioDto, HistorialInventario>()
            .AfterMap((src, dest) =>
            {
                dest.Cantidad = new CantidadVO(src.Cantidad);
                dest.FechaMovimiento = new FechaHistoricaVO(src.FechaMovimiento);
                dest.Observaciones = src.Observaciones != null ? new DescripcionVO(src.Observaciones) : null;
            });
    }
}
