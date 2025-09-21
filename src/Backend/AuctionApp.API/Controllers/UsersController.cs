using AuctionApp.API.Data;
using AuctionApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AuctionDbContext _context;

        public UsersController(AuctionDbContext context)
        {
            _context = context;
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Auctions)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/users/current
        [HttpGet("current")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            // En una aplicación real, obtendríamos el ID del usuario del token
            int userId = 1; // Usuario de ejemplo para pruebas

            var user = await _context.Users
                .Include(u => u.Auctions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}