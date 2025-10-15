using Api.DTOs.Pagos;
using Api.DTOs.Facturas;
using Api.DTOs.MetodosPago;
using Api.DTOs.EstadosPago;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class PagoProfile : Profile
{
    public PagoProfile()
    {
        // Entidad -> DTO simple
        CreateMap<Pago, PagoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.FacturaId, opt => opt.MapFrom(src => src.FacturaId.Value))
            .ForMember(dest => dest.MetodoPagoId, opt => opt.MapFrom(src => src.MetodoPagoId.Value))
            .ForMember(dest => dest.EstadoPagoId, opt => opt.MapFrom(src => src.EstadoPagoId.Value))
            .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto.Value))
            .ForMember(dest => dest.FechaPago, opt => opt.MapFrom(src => src.FechaPago.Value));

        // Entidad -> DTO detallado
        CreateMap<Pago, PagoDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto.Value))
            .ForMember(dest => dest.FechaPago, opt => opt.MapFrom(src => src.FechaPago.Value))
            .ForMember(dest => dest.Factura, opt => opt.MapFrom(src => src.Factura))
            .ForMember(dest => dest.MetodoPago, opt => opt.MapFrom(src => src.MetodoPago))
            .ForMember(dest => dest.EstadoPago, opt => opt.MapFrom(src => src.EstadoPago));

        // Crear DTO -> Entidad
        CreateMap<CreatePagoDto, Pago>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // generado automáticamente
                dest.FacturaId = new IdVO(src.FacturaId);
                dest.MetodoPagoId = new IdVO(src.MetodoPagoId);
                dest.EstadoPagoId = new IdVO(src.EstadoPagoId);
                dest.Monto = new DineroVO(src.Monto);
                dest.FechaPago = new FechaHistoricaVO(src.FechaPago);
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdatePagoDto, Pago>()
            .AfterMap((src, dest) =>
            {
                dest.FacturaId = new IdVO(src.FacturaId);
                dest.MetodoPagoId = new IdVO(src.MetodoPagoId);
                dest.EstadoPagoId = new IdVO(src.EstadoPagoId);
                dest.Monto = new DineroVO(src.Monto);
                dest.FechaPago = new FechaHistoricaVO(src.FechaPago);
            });
    }
}
