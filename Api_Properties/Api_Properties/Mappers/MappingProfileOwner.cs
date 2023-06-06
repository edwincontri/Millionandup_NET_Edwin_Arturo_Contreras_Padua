using Api_Properties.Models;
using Api_Properties.Models.DataTransferObjects;
using AutoMapper;

namespace Api_Properties.Mappers
{
    public class MappingProfileOwner : Profile
    {
        public MappingProfileOwner()
        
        {
            CreateMap<PropertyDto, Property>()
                .ForMember(dest => dest.PkProperty, opt => opt.MapFrom(src => src.IdProperty))
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PropertyAddress, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.PropertyPrice, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.PropertyCodeInternal, opt => opt.MapFrom(src => src.CodeInternal))
                .ForMember(dest => dest.PropertyYear, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.FkOwner, opt => opt.MapFrom(src => src.IdOwner));
        }
    }
}
