using Microsoft.AspNetCore.SignalR;
using vaccine_chain_bk.DTO.Dht11;

namespace vaccine_chain_bk.Hubs
{
    public sealed class TemperatureHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined!");
        }
    }
}
