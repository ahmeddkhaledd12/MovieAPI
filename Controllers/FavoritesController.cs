using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FavoritesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetFavorites()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return Unauthorized();

            return await _context.Favorites
                .Where(f => f.UserId == user.Id)
                .Include(f => f.Movie)
                .Select(f => new {
                    f.Id,
                    f.MovieId,
                    Movie = new { f.Movie.Title, f.Movie.Genre, f.Movie.PosterUrl }
                })
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddFavorite([FromBody] FavoriteRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return Unauthorized();

            // Check if movie exists
            var movie = await _context.Movies.FindAsync(request.MovieId);
            if (movie == null) return BadRequest("Movie not found");

            // Check if already in favorites
            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.MovieId == request.MovieId);

            if (existingFavorite != null) return BadRequest("Movie already in favorites");

            var favorite = new Favorite
            {
                UserId = user.Id,
                MovieId = request.MovieId
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Added to favorites",
                id = favorite.Id,
                movieId = favorite.MovieId
            });
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> RemoveFavorite(int movieId)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return Unauthorized();

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.MovieId == movieId);

            if (favorite == null) return NotFound("Favorite not found");

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Removed from favorites" });
        }

        public class FavoriteRequest
        {
            public int MovieId { get; set; }
        }
    }
}