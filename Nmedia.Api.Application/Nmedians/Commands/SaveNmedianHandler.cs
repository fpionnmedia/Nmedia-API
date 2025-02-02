﻿#nullable enable
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application.Exceptions;
using Nmedia.Api.Application.Nmedians.Models;
using Nmedia.Domain.Nmedians;
using Nmedia.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Nmedians.Commands
{
  public class SaveNmedianHandler : IRequestHandler<SaveNmedian, NmedianModel>
  {
    private readonly IApplicationContext _appContext;
    private readonly IDateTimeOffset _dateTimeOffset;
    private readonly INmediaContext _dbContext;
    private readonly IGuid _guid;
    private readonly IMapper _mapper;

    public SaveNmedianHandler(
      IApplicationContext appContext,
      IDateTimeOffset dateTimeOffset,
      INmediaContext dbContext,
      IGuid guid,
      IMapper mapper
    )
    {
      _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
      _dateTimeOffset = dateTimeOffset ?? throw new ArgumentNullException(nameof(dateTimeOffset));
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
      _guid = guid ?? throw new ArgumentNullException(nameof(guid));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<NmedianModel> Handle(SaveNmedian request, CancellationToken cancellationToken)
    {
      if (_appContext.User.Role != Role.Master)
      {
        throw new ForbiddenException();
      }

      Nmedian entity;
      if (request.Id.HasValue)
      {
        entity = await _dbContext.Nmedians
          .SingleOrDefaultAsync(x => x.Uuid == request.Id.Value, cancellationToken)
          ?? throw new NotFoundException();

        entity.Updated = _dateTimeOffset.Now;
      }
      else
      {
        entity = new Nmedian
        {
          Created = _dateTimeOffset.Now,
          Uuid = _guid.NewGuid()
        };

        _dbContext.Nmedians.Add(entity);
      }

      entity.Age = request.Model.Age;
      entity.Hired = request.Model.Hired;
      entity.HourlyRate = request.Model.HourlyRate;
      entity.IsActive = request.Model.IsActive;
      entity.JobTitle = request.Model.JobTitle == null
        ? (JobTitle?)null
        : Enum.Parse<JobTitle>(request.Model.JobTitle);
      entity.Name = request.Model.Name?.CleanTrim() ?? throw new InvalidOperationException("The Name is required.");
      entity.Picture = request.Model.Picture?.CleanTrim();
      entity.Slug = request.Model.Slug?.CleanTrim();

      await _dbContext.SaveChangesAsync(cancellationToken);

      return _mapper.Map<NmedianModel>(entity);
    }
  }
}
