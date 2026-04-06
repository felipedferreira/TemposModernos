using Acme.Cinedex.MoviesService.WebService.API.Movies.Create;
using Acme.Cinedex.MoviesService.WebService.API.Movies.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Cinedex.MoviesService.WebService.API.Movies;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var titanic = new GetAllMoviesResponse(Guid.NewGuid(), "Titanic");
        List<GetAllMoviesResponse> movies = [titanic];
        return Ok(movies);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateMovieRequest request)
    {
        var id = Guid.NewGuid();
        return CreatedAtAction(nameof(GetAll), id);
    }
}
