using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            // Return users without sensitive data
            return await _context.Users
                .Select(u => new { u.Id, u.Username, u.Email })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetUser(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new { u.Id, u.Username, u.Email })
                .FirstOrDefaultAsync();

            return user == null ? NotFound() : user;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<object>> GetUserProfile()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users
                .Where(u => u.Email == userEmail)
                .Select(u => new { u.Id, u.Username, u.Email })
                .FirstOrDefaultAsync();

            return user == null ? Unauthorized() : user;
        }

        // Remove POST, PUT, DELETE for security (use Auth/register instead)
    }
}