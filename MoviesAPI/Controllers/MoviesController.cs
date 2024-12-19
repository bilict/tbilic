using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;
using MoviesAPI.Data;
 
namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MoviesService _moviesService;
 
        public MovieController(MoviesService moviesService)
        {
            _moviesService = moviesService;
        }
 
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _moviesService.GetAllMoviesAsync();
            return Ok(movies);
        }
 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie == null)
                return NotFound();
            return Ok(movie);
        }
 
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] Movie movie)
        {
            await _moviesService.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (id != movie.Id)
                return BadRequest();
 
            await _moviesService.UpdateMovieAsync(movie);
            return NoContent();
        }
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _moviesService.DeleteMovieAsync(id);
            return NoContent();
        }
    }
}
has context menu