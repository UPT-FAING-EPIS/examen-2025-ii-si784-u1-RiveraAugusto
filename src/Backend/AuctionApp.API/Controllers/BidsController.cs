using AuctionApp.API.Data;
using AuctionApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly AuctionDbContext _context;

        public BidsController(AuctionDbContext context)
        {
            _context = context;
        }

        // GET: api/bids?auctionId=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBids([FromQuery] int auctionId)
        {
            return await _context.Bids
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.Amount)
                .ToListAsync();
        }

        // POST: api/bids
        [HttpPost]
        public async Task<ActionResult<Bid>> PostBid(Bid bid)
        {
            // En una aplicación real, obtendríamos el ID del usuario del token
            bid.BidderId = 2; // Usuario de ejemplo
            bid.BidTime = DateTime.UtcNow;

            var auction = await _context.Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == bid.AuctionId);

            if (auction == null)
            {
                return BadRequest("La subasta no existe");
            }

            if (DateTime.UtcNow > auction.EndDate)
            {
                return BadRequest("La subasta ha finalizado");
            }

            var highestBid = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
            if (highestBid != null && bid.Amount <= highestBid.Amount)
            {
                return BadRequest("La oferta debe ser mayor que la oferta más alta actual");
            }

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBids), new { auctionId = bid.AuctionId }, bid);
        }

        // GET: api/bids/user
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<Bid>>> GetUserBids()
        {
            // En una aplicación real, obtendríamos el ID del usuario del token
            int userId = 2; // Usuario de ejemplo

            return await _context.Bids
                .Where(b => b.BidderId == userId)
                .Include(b => b.Auction)
                .OrderByDescending(b => b.BidTime)
                .ToListAsync();
        }
    }
}