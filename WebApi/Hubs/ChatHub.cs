using System.Security.Claims;
using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CarShopFinal.WebApi.Hubs;
[Authorize]

public class ChatHub:Hub
{
    private readonly IChatMessageRepository _chatMessageRepository;
    public ChatHub(IChatMessageRepository repository)
    {
        _chatMessageRepository = repository;
    }

    public async Task JoinConversation(string listingID, string userID)
    {
        var room = GetRoomName(GetMe(), Guid.Parse(userID), Guid.Parse(listingID));
        await Groups.AddToGroupAsync(Context.ConnectionId, room);
    }

    public async Task LeaveConversation(string listingID, string userID)
    {
        var room = GetRoomName(GetMe(), Guid.Parse(userID), Guid.Parse(listingID));
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
    }

    public async Task SendMessage(string receiverID, string listingID, string message)
    {
        var senderID = GetMe();
        var receiver = Guid.Parse(receiverID);
        var listing = Guid.Parse(listingID);
        var Message = new ChatMessage(senderID, receiver, listing, message);
        await _chatMessageRepository.SaveMessageAsync(Message);
        
        var room = GetRoomName(senderID, receiver, listing);
        
        await Clients.Group(room).SendAsync("ReceiveMessage", Message);
        
    }

    public async Task MarkAsRead(string senderId, string listingID)
    {
        var sender = Guid.Parse(senderId);
        var receiver = GetMe();
        var listing = Guid.Parse(listingID);
        await _chatMessageRepository.MarkAsReadAsync(receiver, sender, listing);
        var room = GetRoomName(receiver, sender, listing);
        await Clients.Group(room).SendAsync("ReceiveMarkAsRead", sender);
    }

    private static string GetRoomName(Guid me, Guid userID, Guid listingID)
    {
        var id = new[] { me, userID }.OrderBy(x => x).ToArray();
        return $"chat_{listingID}_{id[0]}_{id[1]}";
    }

    private Guid GetMe()
    {
        var claim = Context.User?.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null)
            throw new HubException("User is not authenticatate");
        return Guid.Parse(claim.Value);
    }
}