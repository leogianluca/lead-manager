using AutoMapper;
using LeadManager.Application.DTOs;
using LeadManager.Domain.Entities;

namespace LeadManager.API.Mapping
{
    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            CreateMap<Lead, LeadDto>()
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.Value))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.Value))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
               .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Number))
               .ForMember(dest => dest.Suburb, opt => opt.MapFrom(src => src.Suburb.Value))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (int)src.Category))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status));
        }
    }
}
