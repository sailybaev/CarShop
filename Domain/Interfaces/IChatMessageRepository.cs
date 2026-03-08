using CarShopFinal.Domain.Models;

namespace CarShopFinal.Domain.Interfaces;

public interface IChatMessageRepository
{
    Task SaveMessageAsync(ChatMessage message);
    Task<List<ChatMessage>> GetAllMessagesAsync(Guid sernderID, Guid receiverID, Guid listingID);
    Task MarkAsReadAsync(Guid sernderID, Guid receiverID, Guid listingID);
}