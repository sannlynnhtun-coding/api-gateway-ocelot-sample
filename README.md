# DotNet8.ApiGateway

Sure! Below is the complete setup for handling multiple endpoints with different JSON responses in an ASP.NET Core Web API project using .NET 8 and Ocelot.

### Step 1: Create Microservices

#### `Article.Api` Project

1. Create a new ASP.NET Core Web API project.
2. Define the Article model and controller.

```csharp
// Models/Article.cs
public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}

// Controllers/ArticlesController.cs
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
```

#### `Writer.Api` Project

1. Create another ASP.NET Core Web API project.
2. Define the Writer model and controller.

```csharp
// Models/Writer.cs
public class Writer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
}

// Controllers/WritersController.cs
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
```

### Step 2: Create API Gateway Project

1. Create a new ASP.NET Core Web API project for the API Gateway.
2. Install the Ocelot package:
   ```
   dotnet add package Ocelot
   ```

3. Configure `ocelot.json` for routing:

```json
{
  "Routes": [
    {
      "UpstreamPathTemplate": "/gateway/articles",
      "DownstreamPathTemplate": "/api/articles",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 7201
        }
      ]
    },
    {
      "UpstreamPathTemplate": "/gateway/writers",
      "DownstreamPathTemplate": "/api/writers",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 7265
        }
      ]
    }
  ],
  "GlobalConfiguration": {
    "BaseUrl": "http://localhost:7152"
  }
}
```

4. Configure `Program.cs` to use Ocelot:

```csharp
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

var app = builder.Build();

await app.UseOcelot();

app.Run();
```

### Step 3: Run the Projects

1. Set up the solution to run all three projects (Article.Api, Writer.Api, and API Gateway) simultaneously.
2. Ensure each project runs on its designated port:
   - Article.Api: http://localhost:7201
   - Writer.Api: http://localhost:7265
   - API Gateway: http://localhost:7152

3. Test the endpoints:
   - http://localhost:7152/gateway/articles
   - http://localhost:7152/gateway/writers

This setup routes requests through the API Gateway to the respective microservices, handling different JSON endpoints effectively.