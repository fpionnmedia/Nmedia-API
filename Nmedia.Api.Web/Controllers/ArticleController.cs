#nullable enable
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nmedia.Api.Application.Articles.Commands;
using Nmedia.Api.Application.Articles.Models;
using Nmedia.Api.Application.Articles.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Web.Controllers
{
  [ApiController]
  [Route("articles")]
  public class ArticleController : Controller
  {
    private readonly IMediator _mediator;

    public ArticleController(IMediator mediator)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<ActionResult<ArticleModel>> CreateAsync(
      [FromBody] SaveArticleModel model,
      CancellationToken cancellationToken
    )
    {
      ArticleModel result = await _mediator.Send(new SaveArticle(model), cancellationToken);
      var uri = new Uri($"/articles/{result.Id}", UriKind.Relative);

      return Created(uri, result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
      await _mediator.Send(new DeleteArticle(id), cancellationToken);
      return NoContent();
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleItemModel>>> GetByIdAsync(CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new GetAllArticles(), cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
      return Ok(await _mediator.Send(new GetArticleById(id), cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ArticleModel>> UpdateAsync(
      Guid id,
      [FromBody] SaveArticleModel model,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new SaveArticle(model, id), cancellationToken));
    }
  }
}
