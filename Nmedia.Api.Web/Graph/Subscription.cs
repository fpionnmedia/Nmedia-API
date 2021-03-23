using HotChocolate;
using HotChocolate.Types;
using Nmedia.Domain.Articles;

namespace Nmedia.Api.Web.Graph
{
  public class Subscription
  {
    [Subscribe]
    [Topic]
    public Article OnArticleAdded([EventMessage] Article entity)
    {
      return entity;
    }
  }
}
