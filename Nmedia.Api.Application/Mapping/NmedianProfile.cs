#nullable enable
using AutoMapper;
using Nmedia.Api.Application.Nmedians.Models;
using Nmedia.Domain.Nmedians;

namespace Nmedia.Api.Application.Mapping
{
  public class NmedianProfile : Profile
  {
    public NmedianProfile()
    {
      CreateMap<Nmedian, NmedianItemModel>()
        .ForMember(x => x.Id, x => x.MapFrom(y => y.Uuid))
        .ForMember(x => x.Updated, x => x.MapFrom(y => y.Updated ?? y.Created));
      CreateMap<Nmedian, NmedianModel>()
        .ForMember(x => x.Id, x => x.MapFrom(y => y.Uuid));
    }
  }
}
