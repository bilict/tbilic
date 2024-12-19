using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;
using MoviesAPI.Data;
using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Retrieves all movies.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _moviesService.GetAllMoviesAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a movie by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            try
            {
                var movie = await _moviesService.GetMovieByIdAsync(id);
                if (movie == null)
                    return NotFound();
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new movie.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody][Required] Movie movie)
        {
            if (movie == null)
                return BadRequest("Movie cannot be null.");

            try
            {
                await _moviesService.AddMovieAsync(movie);
                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing movie.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody][Required] Movie movie)
        {
            if (movie == null)
                return BadRequest("Movie cannot be null.");

            if (id != movie.Id)
                return BadRequest("ID mismatch.");

            try
            {
                var existingMovie = await _moviesService.GetMovieByIdAsync(id);
                if (existingMovie == null)
                    return NotFound();

                await _moviesService.UpdateMovieAsync(movie);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a movie by its ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                var existingMovie = await _moviesService.GetMovieByIdAsync(id);
                if (existingMovie == null)
                    return NotFound();

                await _moviesService.DeleteMovieAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
