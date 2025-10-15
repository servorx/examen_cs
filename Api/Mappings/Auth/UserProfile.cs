using AutoMapper;
using Domain.Entities.Auth;
using Api.DTOs.Auth;
using System.Linq;

namespace Api.Mappings.Auth;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Mapeo de UserMember -> DataUserDto
        CreateMap<UserMember, DataUserDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Rols.Select(r => r.Name).ToList()))
            // Ignoramos propiedades de autenticación si no se usan aquí
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.IsAuthenticated, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
            .ForMember(dest => dest.RefreshTokenExpiration, opt => opt.Ignore());
    }
}
