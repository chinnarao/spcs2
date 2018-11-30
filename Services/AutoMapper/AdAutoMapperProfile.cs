using AutoMapper;
using Share.Models.Ad.Entities;
using Share.Models.Ad.Dtos;
using System;
using GeoAPI.Geometries;
using NetTopologySuite;

namespace Services.AutoMapper
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
