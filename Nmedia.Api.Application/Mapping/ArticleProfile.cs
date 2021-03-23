#nullable enable
using AutoMapper;
using Nmedia.Api.Application.Articles.Models;
using Nmedia.Domain.Articles;
using System;

namespace Nmedia.Api.Application.Mapping
{
  public class ArticleProfile : Profile
  {
    public ArticleProfile()
    {
      CreateMap<Article, ArticleItemModel>()
        .ForMember(x => x.Id, x => x.MapFrom(y => y.Uuid))
        .ForMember(x => x.Updated, x => x.MapFrom(y => y.Updated ?? y.Created));
      CreateMap<Article, ArticleModel>()
        .ForMember(x => x.Id, x => x.MapFrom(y => y.Uuid))
        .ForMember(x => x.NmedianId, x => x.MapFrom(GetNmedianId));
    }

    private static Guid? GetNmedianId(Article entity, ArticleModel model)
    {
      return entity.Nmedian?.Uuid;
    }
  }
}
