using System.Xml.Serialization;
using AutoMapper;
using Communication.Requests;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Services.Mapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        
    }

    private void RequestToDomain()
    {
        CreateMap<UserRequest, User>()
            .ForMember(opt => opt.Password, src => src.Ignore())
            .ForMember(opt => opt.Role, src =>src.MapFrom(_ => UserRole.User) );

    }
    
}