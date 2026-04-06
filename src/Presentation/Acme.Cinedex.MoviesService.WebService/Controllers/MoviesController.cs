using Microsoft.AspNetCore.Mvc;

namespace Acme.Cinedex.MoviesService.WebService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("Hello from MoviesController");
}