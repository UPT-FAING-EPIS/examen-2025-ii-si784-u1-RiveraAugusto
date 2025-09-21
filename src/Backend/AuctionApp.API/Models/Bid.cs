using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuctionApp.API.Models
{
    public class Bid
    {
        public int Id { get; set; }
        
        [Required]
        public int AuctionId { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        public decimal Amount { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        [JsonIgnore]
        public Auction? Auction { get; set; }
    }
}