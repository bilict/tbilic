using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    public class MoviesController : ControllerBase
    {
        private readonly MoviesService _moviesService;

        public MoviesController(MoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        public IActionResult GetAllMovies() => Ok(_moviesService.GetAllMovies());

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id) => Ok(_moviesService.GetMovieById(id));

        [HttpPost]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            _moviesService.AddMovie(movie);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] Movie updatedMovie)
        {
            _moviesService.UpdateMovie(id, updatedMovie);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            _moviesService.DeleteMovie(id);
            return Ok();
        }
    }
}


