using AutoMapper;
using Share.Models.Ad.Entities;
using Share.Models.Ad.Dtos;

namespace Services.Ad
{
    public class AdAutoMapperProfile : Profile
    {
        public AdAutoMapperProfile()
        {
            CreateMap<Share.Models.Ad.Entities.Ad, AdDto>()
                .ReverseMap()
                .ForMember(dest => dest.AddressLocation, opt => opt.Ignore());
        }
    }
}
