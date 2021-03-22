#nullable enable
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nmedia.Api.Application.Nmedians.Commands;
using Nmedia.Api.Application.Nmedians.Models;
using Nmedia.Api.Application.Nmedians.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Web.Controllers
{
  [ApiController]
  [Route("nmedians")]
  public class NmedianController : Controller
  {
    private readonly IMediator _mediator;

    public NmedianController(IMediator mediator)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<ActionResult<NmedianModel>> CreateAsync(
      [FromBody] SaveNmedianModel model,
      CancellationToken cancellationToken
    )
    {
      NmedianModel result = await _mediator.Send(new SaveNmedian(model), cancellationToken);
      var uri = new Uri($"/nmedians/{result.Id}", UriKind.Relative);

      return Created(uri, result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
      await _mediator.Send(new DeleteNmedian(id), cancellationToken);
      return NoContent();
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NmedianItemModel>>> GetByIdAsync(CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new GetAllNmedians(), cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NmedianModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new GetNmedianById(id), cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<NmedianModel>> UpdateAsync(
      Guid id,
      [FromBody] SaveNmedianModel model,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new SaveNmedian(model, id), cancellationToken));
    }
  }
}
