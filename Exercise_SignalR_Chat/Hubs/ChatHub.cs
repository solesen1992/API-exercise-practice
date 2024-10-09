using Microsoft.AspNetCore.SignalR;

namespace Exercise_SignalR_Chat.Hubs
{
    /* 
     * CREDIT: Code is from this website: 
     * https://learn.microsoft.com/da-dk/aspnet/core/tutorials/signalr?view=aspnetcore-8.0&tabs=visual-studio&WT.mc_id=dotnet-35129-website
     */
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
