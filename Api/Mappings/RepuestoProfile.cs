using Api.DTOs.Repuestos;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class RepuestoProfile : Profile
{
    public RepuestoProfile()
    {
        // Entidad -> DTO de respuesta simple
        CreateMap<Repuesto, RepuestoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo.Value))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion.Value))
            .ForMember(dest => dest.CantidadStock, opt => opt.MapFrom(src => src.CantidadStock.Value))
            .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario.Value))
            .ForMember(dest => dest.ProveedorId, opt => opt.MapFrom(src => src.ProveedorId != null ? src.ProveedorId.Value : (int?)null));

        // Entidad -> DTO detallado
        CreateMap<Repuesto, RepuestoDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo.Value))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion.Value))
            .ForMember(dest => dest.CantidadStock, opt => opt.MapFrom(src => src.CantidadStock.Value))
            .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario.Value))
            .ForMember(dest => dest.ProveedorId, opt => opt.MapFrom(src => src.ProveedorId != null ? src.ProveedorId.Value : (int?)null))
            .ForMember(dest => dest.ProveedorNombre, opt => opt.MapFrom(src => src.Proveedor != null ? src.Proveedor.Nombre.Value : null))
            .ForMember(dest => dest.Historiales, opt => opt.MapFrom(src => src.Historiales.Select(h => h.Id.Value.ToString())))
            .ForMember(dest => dest.DetallesOrden, opt => opt.MapFrom(src => src.DetallesOrden.Select(d => d.RepuestoId.Value.ToString())));

        // Crear DTO -> Entidad
        CreateMap<CreateRepuestoDto, Repuesto>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // se generará automáticamente
                dest.Codigo = new CodigoRepuestoVO(src.Codigo);
                dest.Descripcion = new DescripcionVO(src.Descripcion);
                dest.CantidadStock = new CantidadVO(src.CantidadStock);
                dest.PrecioUnitario = new DineroVO(src.PrecioUnitario);
                dest.ProveedorId = src.ProveedorId != null ? new IdVO(src.ProveedorId.Value) : null;
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateRepuestoDto, Repuesto>()
            .AfterMap((src, dest) =>
            {
                dest.Codigo = new CodigoRepuestoVO(src.Codigo);
                dest.Descripcion = new DescripcionVO(src.Descripcion);
                dest.CantidadStock = new CantidadVO(src.CantidadStock);
                dest.PrecioUnitario = new DineroVO(src.PrecioUnitario);
                dest.ProveedorId = src.ProveedorId != null ? new IdVO(src.ProveedorId.Value) : null;
            });
    }
}
