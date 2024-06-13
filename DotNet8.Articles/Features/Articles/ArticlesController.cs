using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.Articles.Features.Articles
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private static readonly List<Article> Articles = new()
    {
        new Article { Id = 1, Title = "Article 1", Content = "Content 1" },
        new Article { Id = 2, Title = "Article 2", Content = "Content 2" }
    };

        [HttpGet]
        public IActionResult GetArticles() => Ok(Articles);
    }
}
