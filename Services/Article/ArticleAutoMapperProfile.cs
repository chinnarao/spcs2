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
            CreateMap<ArticleLicenseDto, ArticleLicense>().ForMember(dest => dest.Article, opt => opt.Ignore()).ReverseMap();
            CreateMap<ArticleCommitDto, ArticleCommit>().ForMember(dest => dest.Article, opt => opt.Ignore()).ReverseMap();
            CreateMap<ArticleCommentDto, ArticleComment>().ForMember(dest => dest.Article, opt => opt.Ignore()).ReverseMap();

            CreateMap<ArticleDto, Models.Article.Entities.Article>()
                .ForMember(dest => dest.ArticleLicense, opt => opt.MapFrom(src => src.ArticleLicenseDto))
                .ForMember(dest => dest.ArticleCommits, opt => opt.MapFrom(src => src.ArticleCommitDtos))
                .ForMember(dest => dest.ArticleComments, opt => opt.MapFrom(src => src.ArticleCommentDtos));

            CreateMap<Models.Article.Entities.Article, ArticleDto>()
                .ForMember(dest => dest.ArticleLicenseDto, opt => opt.MapFrom(src => src.ArticleLicense))
                .ForMember(dest => dest.ArticleCommitDtos, opt => opt.MapFrom(src => src.ArticleCommits))
                .ForMember(dest => dest.ArticleCommentDtos, opt => opt.MapFrom(src => src.ArticleComments))
                .ForMember(dest => dest.GoogleStorageArticleFileDto, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDateTimeString, opt => opt.Ignore());
        }
    }
}
