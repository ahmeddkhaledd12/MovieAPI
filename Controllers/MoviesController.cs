using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MoviesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies() =>
            await _context.Movies.ToListAsync();

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchMovies([FromQuery] string? title, [FromQuery] string? genre)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(m => m.Title.Contains(title));

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(m => m.Genre.Contains(genre));

            return await query.ToListAsync();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Movie>> CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovies), new { id = movie.Id }, movie);
        }
    }
}