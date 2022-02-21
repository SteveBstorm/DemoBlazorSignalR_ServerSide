using Microsoft.AspNetCore.SignalR;

namespace DemoBlazorSignalR_ServerSide.Hubs
{
    public class ChatHub : Hub
    {
        public async Task FromClient(string userName, string message)
        {
            await Clients.All.SendAsync("FromServer", userName, message);
        }
    }
}
