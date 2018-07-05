using AutoMapper;
using Models.Ad.Entities;
using Models.Ad.Dtos;
using Models.Article.Entities;
using Models.Article.Dtos;

namespace Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ad, AdDto>().ReverseMap();
            CreateMap<Article, ArticleDto>().ReverseMap();
        }
    }
}
