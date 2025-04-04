using Microsoft.AspNetCore.SignalR;

namespace RazorPage
{
    public class OrderHub : Hub
    {
        public async Task SendOrderNotification(string message)
        {
            Console.WriteLine($"Sending order notification: {message}");
            await Clients.All.SendAsync("ReceiveOrderNotification", message);
            Console.WriteLine("Notification sent to all clients.");
        }

        // Optional: Add method to join specific groups based on role
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
    