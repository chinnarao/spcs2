using AutoMapper;
using Models.Ad.Entities;
using Models.Ad.Dtos;

namespace Services.Ad
{
    public class AdAutoMapperProfile : Profile
    {
        public AdAutoMapperProfile()
        {
            CreateMap<Models.Ad.Entities.Ad, AdDto>().ReverseMap();
        }
    }
}
