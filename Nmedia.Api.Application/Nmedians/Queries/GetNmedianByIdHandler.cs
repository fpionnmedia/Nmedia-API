#nullable enable
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application.Exceptions;
using Nmedia.Api.Application.Nmedians.Models;
using Nmedia.Domain.Nmedians;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Nmedians.Queries
{
  public class GetNmedianByIdHandler : IRequestHandler<GetNmedianById, NmedianModel>
  {
    private readonly INmediaContext _dbContext;
    private readonly IMapper _mapper;

    public GetNmedianByIdHandler(INmediaContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<NmedianModel> Handle(GetNmedianById request, CancellationToken cancellationToken)
    {
      Nmedian entity = await _dbContext.Nmedians
        .SingleOrDefaultAsync(x => x.Uuid == request.Id, cancellationToken)
        ?? throw new NotFoundException();

      return _mapper.Map<NmedianModel>(entity);
    }
  }
}
