namespace AuctionApp.API.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal StartingPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SellerId { get; set; }
        public User? Seller { get; set; }
        public int? WinningBidId { get; set; }
        public Bid? WinningBid { get; set; }
        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }
}