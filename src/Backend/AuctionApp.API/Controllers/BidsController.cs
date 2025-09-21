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

        // POST: api/bids
        [HttpPost]
        public async Task<ActionResult<Bid>> CreateBid(Bid bid)
        {
            // Validar que la subasta existe
            var auction = await _context.Auctions.FindAsync(bid.AuctionId);
            if (auction == null)
                return NotFound("Subasta no encontrada");

            // Validar que la subasta está activa
            var now = DateTime.UtcNow;
            if (now < auction.StartDate || now > auction.EndDate)
                return BadRequest("La subasta no está activa");

            // Validar que la oferta es mayor que el precio actual
            var highestBid = await _context.Bids
                .Where(b => b.AuctionId == bid.AuctionId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefaultAsync();

            decimal minAmount = highestBid != null ? highestBid.Amount : auction.StartingPrice;
            if (bid.Amount <= minAmount)
                return BadRequest($"La oferta debe ser mayor a {minAmount}");

            // Guardar la oferta
            bid.CreatedAt = DateTime.UtcNow;
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            // Notificar a través de SignalR (se implementará después)

            return CreatedAtAction(nameof(GetBidsByAuction), new { auctionId = bid.AuctionId }, bid);
        }

        // GET: api/bids?auctionId=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidsByAuction([FromQuery] int auctionId)
        {
            return await _context.Bids
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.Amount)
                .ToListAsync();
        }

        // GET: api/bids/user
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<Bid>>> GetUserBids()
        {
            // En una implementación real, obtendríamos el ID del usuario del token
            string userId = "user123"; // Temporal para pruebas

            return await _context.Bids
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }
    }
}