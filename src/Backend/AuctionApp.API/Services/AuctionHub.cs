using Microsoft.AspNetCore.SignalR;
using AuctionApp.API.Models;

namespace AuctionApp.API.Services
{
    public class AuctionHub : Hub
    {
        public async Task JoinAuctionGroup(int auctionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"auction_{auctionId}");
        }

        public async Task LeaveAuctionGroup(int auctionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"auction_{auctionId}");
        }

        public async Task NotifyNewBid(Bid bid)
        {
            await Clients.Group($"auction_{bid.AuctionId}").SendAsync("ReceiveNewBid", bid);
        }
    }
}