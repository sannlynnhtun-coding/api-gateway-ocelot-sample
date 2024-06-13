using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.Writers.Features.Writers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WritersController : ControllerBase
    {
        private static readonly List<Writer> Writers = new()
    {
        new Writer { Id = 1, Name = "Writer 1", Bio = "Bio 1" },
        new Writer { Id = 2, Name = "Writer 2", Bio = "Bio 2" }
    };

        [HttpGet]
        public IActionResult GetWriters() => Ok(Writers);
    }
}
