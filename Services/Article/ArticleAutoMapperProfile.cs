using AutoMapper;
using Models.Ad.Entities;
using Models.Ad.Dtos;
using Models.Article.Entities;
using Models.Article.Dtos;

namespace Services.Article
{
    public class ArticleAutoMapperProfile : Profile
    {
        public ArticleAutoMapperProfile()
        {
            CreateMap<ArticleLicenseDto, ArticleLicense>()
                .ForMember(dest => dest.Article, opt => opt.Ignore());

            CreateMap<ArticleCommitDto, ArticleCommit>()
                .ForMember(dest => dest.Article, opt => opt.Ignore());

            CreateMap<ArticleCommentDto, ArticleComment>()
                .ForMember(dest => dest.Article, opt => opt.Ignore());

            CreateMap<ArticleDto, Models.Article.Entities.Article>()
                .ForMember(dest => dest.ArticleLicense, opt => opt.MapFrom(src => src.ArticleLicenseDto))
                .ForMember(dest => dest.ArticleCommits, opt => opt.MapFrom(src => src.ArticleCommitDtos))
                .ReverseMap();

            //CreateMap<Models.Article.Entities.Article, ArticleDto>()
            //    .ReverseMap()
            //    .ForMember(dest => dest.ArticleLicense, opt => opt.MapFrom(src => src.ArticleLicenseDto))
            //    .ForMember(dest => dest.ArticleCommits, opt => opt.MapFrom(src => src.ArticleCommitDtos));
        }
    }
}
