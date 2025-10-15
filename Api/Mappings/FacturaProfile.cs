using Api.DTOs.Facturas;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class FacturaProfile : Profile
{
    public FacturaProfile()
    {
        // Entidad -> DTO
        CreateMap<Factura, FacturaDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.OrdenServicioId, opt => opt.MapFrom(src => src.OrdenServicioId.Value))
            .ForMember(dest => dest.MontoRepuestos, opt => opt.MapFrom(src => src.MontoRepuestos.Value))
            .ForMember(dest => dest.ManoObra, opt => opt.MapFrom(src => src.ManoObra.Value))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total.Value))
            .ForMember(dest => dest.FechaGeneracion, opt => opt.MapFrom(src => src.FechaGeneracion));

        // Crear DTO -> Entidad
        CreateMap<CreateFacturaDto, Factura>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // generado automáticamente
                dest.OrdenServicioId = new IdVO(src.OrdenServicioId);
                dest.MontoRepuestos = new DineroVO(src.MontoRepuestos);
                dest.ManoObra = new DineroVO(src.ManoObra);
                dest.Total = new DineroVO(src.Total);
                dest.FechaGeneracion = new FechaHistoricaVO(src.FechaGeneracion);
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateFacturaDto, Factura>()
            .AfterMap((src, dest) =>
            {
                dest.MontoRepuestos = new DineroVO(src.MontoRepuestos);
                dest.ManoObra = new DineroVO(src.ManoObra);
                dest.Total = new DineroVO(src.Total);
                dest.FechaGeneracion = new FechaHistoricaVO(src.FechaGeneracion);
            });

        // Entidad -> Detail DTO
        CreateMap<Factura, FacturaDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.OrdenServicioId, opt => opt.MapFrom(src => src.OrdenServicioId.Value))
            .ForMember(dest => dest.MontoRepuestos, opt => opt.MapFrom(src => src.MontoRepuestos.Value))
            .ForMember(dest => dest.ManoObra, opt => opt.MapFrom(src => src.ManoObra.Value))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total.Value))
            .ForMember(dest => dest.FechaGeneracion, opt => opt.MapFrom(src => src.FechaGeneracion.Value))
            // Mapeo de colecciones relacionadas usando los DTOs de cada entidad
            .ForMember(dest => dest.OrdenServicio, opt => opt.MapFrom(src => src.OrdenServicio))
            .ForMember(dest => dest.Pagos, opt => opt.MapFrom(src => src.Pagos));
    }
}
