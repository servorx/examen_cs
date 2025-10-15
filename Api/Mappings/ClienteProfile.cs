using Api.DTOs.Clientes;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Mappings;

public sealed class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        // Entidad -> DTO
        CreateMap<Cliente, ClienteDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value))
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo.Value))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono.Value))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion.Value))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive.Value))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        // Crear DTO -> Entidad
        CreateMap<CreateClienteDto, Cliente>()
            .AfterMap((src, dest) =>
            {
                dest.Id = new IdVO(0); // se generará automáticamente
                dest.Nombre = new NombreVO(src.Nombre);
                dest.Correo = new CorreoVO(src.Correo);
                dest.Telefono = new TelefonoVO(src.Telefono);
                dest.Direccion = new DireccionVO(src.Direccion);
                dest.IsActive = new EstadoVO(src.IsActive);
                dest.UserId = src.UserId;
            });

        // Actualizar DTO -> Entidad
        CreateMap<UpdateClienteDto, Cliente>()
            .AfterMap((src, dest) =>
            {
                if (src.Nombre != null) dest.Nombre = new NombreVO(src.Nombre);
                if (src.Correo != null) dest.Correo = new CorreoVO(src.Correo);
                if (src.Telefono != null) dest.Telefono = new TelefonoVO(src.Telefono);
                if (src.Direccion != null) dest.Direccion = new DireccionVO(src.Direccion);
                dest.IsActive = new EstadoVO(src.IsActive);
            });
    }
}
