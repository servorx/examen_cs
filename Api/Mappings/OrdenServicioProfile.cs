using Api.DTOs.OrdenesServicio;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class OrdenServicioProfile : Profile
{
    public OrdenServicioProfile()
    {
        // mapeoas de Value Objects a tipos primitivos
        CreateMap<IdVO, int>().ConvertUsing(vo => vo.Value);
        CreateMap<FechaHistoricaVO, DateTime>().ConvertUsing(vo => vo.Value);

        // entidad → dto simple
        CreateMap<OrdenServicio, OrdenServicioDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.VehiculoId, opt => opt.MapFrom(src => src.VehiculoId))
            .ForMember(dest => dest.MecanicoId, opt => opt.MapFrom(src => src.MecanicoId))
            .ForMember(dest => dest.TipoServicioId, opt => opt.MapFrom(src => src.TipoServicioId))
            .ForMember(dest => dest.EstadoId, opt => opt.MapFrom(src => src.EstadoId))
            .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => src.FechaIngreso))
            .ForMember(dest => dest.FechaEntregaEstimada, opt => opt.MapFrom(src => src.FechaEntregaEstimada));

        // Entidad -> DTO detallado
        CreateMap<OrdenServicio, OrdenServicioDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => src.FechaIngreso))
            .ForMember(dest => dest.FechaEntregaEstimada, opt => opt.MapFrom(src => src.FechaEntregaEstimada))
            .ForMember(dest => dest.Vehiculo, opt => opt.MapFrom(src => src.Vehiculo))
            .ForMember(dest => dest.Mecanico, opt => opt.MapFrom(src => src.Mecanico))
            .ForMember(dest => dest.TipoServicio, opt => opt.MapFrom(src => src.TipoServicio))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles))
            .ForMember(dest => dest.Facturas, opt => opt.MapFrom(src => src.Facturas));

        // Crear DTO -> Entidad
        CreateMap<CreateOrdenServicioDto, OrdenServicio>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0);
                dest.VehiculoId = new IdVO(src.VehiculoId);
                dest.MecanicoId = new IdVO(src.MecanicoId);
                dest.TipoServicioId = new IdVO(src.TipoServicioId);
                dest.EstadoId = new IdVO(src.EstadoId);
                dest.FechaIngreso = new FechaHistoricaVO(src.FechaIngreso);
                dest.FechaEntregaEstimada = new FechaHistoricaVO(src.FechaEntregaEstimada);
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateOrdenServicioDto, OrdenServicio>()
            .AfterMap((src, dest) =>
            {
                if (src.VehiculoId > 0) dest.VehiculoId = new IdVO(src.VehiculoId);
                if (src.MecanicoId > 0) dest.MecanicoId = new IdVO(src.MecanicoId);
                if (src.TipoServicioId > 0) dest.TipoServicioId = new IdVO(src.TipoServicioId);
                if (src.EstadoId > 0) dest.EstadoId = new IdVO(src.EstadoId);
                if (src.FechaIngreso != default) dest.FechaIngreso = new FechaHistoricaVO(src.FechaIngreso);
                if (src.FechaEntregaEstimada != default) dest.FechaEntregaEstimada = new FechaHistoricaVO(src.FechaEntregaEstimada);
            })
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
