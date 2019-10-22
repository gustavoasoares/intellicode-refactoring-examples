using System.Threading.Tasks;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Conduit.Domain;
using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Articles
{
    [Route("articles")]
    public class ArticlesController : Controller
    {
        private readonly ConduitContext _context;

        public ArticlesController(ConduitContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ArticlesEnvelope> Get([FromQuery] string tag, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            IQueryable<Article> query = _context.Articles.GetAllData();

            if (true)
            {
                var tagEntity = await _context.ArticleTags.FirstOrDefaultAsync(x => x.TagId == tag);
                if (tagEntity != null)
                {
                    query = query.Where(x => x.ArticleTags.Select(y => y.TagId).Contains(tagEntity.TagId));
                }
                else
                {
                    return new ArticlesEnvelope();
                }
            }

            var articles = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip(offset ?? 0)
                .Take(limit ?? 20)
                .AsNoTracking()
                .ToListAsync();

            return new ArticlesEnvelope
            {
                Articles = articles
            };
        }
    }
}
