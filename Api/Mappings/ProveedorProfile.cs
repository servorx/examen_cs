using Api.DTOs.Proveedores;
using Api.DTOs.Repuestos;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class ProveedorProfile : Profile
{
    public ProveedorProfile()
    {
        CreateMap<Proveedor, ProveedorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono != null ? src.Telefono.Value : null))
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo != null ? src.Correo.Value : null))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion != null ? src.Direccion.Value : null))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive.Value))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<Proveedor, ProveedorDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono != null ? src.Telefono.Value : null))
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo != null ? src.Correo.Value : null))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion != null ? src.Direccion.Value : null))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive.Value))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Repuestos, opt => opt.MapFrom(src => src.Repuestos));

        CreateMap<CreateProveedorDto, Proveedor>().AfterMap((src, dest) =>
        {
            dest.Id = new IdVO(0);
            dest.Nombre = new NombreVO(src.Nombre);
            dest.Telefono = src.Telefono != null ? new TelefonoVO(src.Telefono) : null;
            dest.Correo = src.Correo != null ? new CorreoVO(src.Correo) : null;
            dest.Direccion = src.Direccion != null ? new DireccionVO(src.Direccion) : null;
            dest.IsActive = new EstadoVO(src.IsActive);
            dest.UserId = src.UserId;
        });

        CreateMap<UpdateProveedorDto, Proveedor>().AfterMap((src, dest) =>
        {
            dest.Nombre = new NombreVO(src.Nombre);
            dest.Telefono = src.Telefono != null ? new TelefonoVO(src.Telefono) : null;
            dest.Correo = src.Correo != null ? new CorreoVO(src.Correo) : null;
            dest.Direccion = src.Direccion != null ? new DireccionVO(src.Direccion) : null;
            dest.IsActive = new EstadoVO(src.IsActive);
            dest.UserId = src.UserId;
        });
    }
}
