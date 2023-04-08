using ChatEncrypt.Server.Hubs;
using EncryptChat.Shared.Model;
using Microsoft.AspNetCore.SignalR;

namespace EncryptChat.Server.Hubs;

public class ChatHub : Hub
{
    private static readonly ConnectionMapping<string> _connections = new();

    public async Task SendNotification(string target, Notification notification)
    {
        if (_connections.GetConnections(target).Any())
            await Clients.Client(_connections.GetConnections(target).First())
                .SendAsync("ReceiveNotification", notification);
    }

    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
    }

    public async Task LeaveRoom(string roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
    }

    public async Task SendMessage(string roomId, string user, string message)
    {
        await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message);
    }

    public override Task OnConnectedAsync()
    {
        if (Context.GetHttpContext()!.Request.Headers.TryGetValue("email", out var email))
            _connections.Add(email.ToString(), Context.ConnectionId);
        return base.OnConnectedAsync();
    }
}