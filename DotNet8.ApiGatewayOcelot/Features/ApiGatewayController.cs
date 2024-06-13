using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace DotNet8.ApiGatewayOcelot.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGatewayController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Hello API Gateway.");
    }
}
