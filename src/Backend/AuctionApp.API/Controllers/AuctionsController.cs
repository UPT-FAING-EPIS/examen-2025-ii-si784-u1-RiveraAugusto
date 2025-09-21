using AuctionApp.API.Data;
using AuctionApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionDbContext _context;

        public AuctionsController(AuctionDbContext context)
        {
            _context = context;
        }

        // GET: api/auctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctions()
        {
            return await _context.Auctions.ToListAsync();
        }

        // GET: api/auctions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auction>> GetAuction(int id)
        {
            var auction = await _context.Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auction == null)
            {
                return NotFound();
            }

            return auction;
        }

        // POST: api/auctions
        [HttpPost]
        public async Task<ActionResult<Auction>> PostAuction(Auction auction)
        {
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuction), new { id = auction.Id }, auction);
        }

        // GET: api/auctions/user
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<Auction>>> GetUserAuctions()
        {
            // En una aplicación real, obtendríamos el ID del usuario del token
            int userId = 1; // Usuario de ejemplo

            return await _context.Auctions
                .Where(a => a.SellerId == userId)
                .ToListAsync();
        }
    }
}