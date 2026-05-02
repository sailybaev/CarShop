using CarShopFinal.Domain.Models;

namespace CarShopFinal.Domain.Interfaces;

public record ConversationRef(Guid ListingId, Guid OtherUserId);

public interface IChatMessageRepository
{
    Task SaveMessageAsync(ChatMessage message);
    Task<List<ChatMessage>> GetAllMessagesAsync(Guid userID, Guid otherUserID, Guid listingID);
    Task MarkAsReadAsync(Guid sernderID, Guid receiverID, Guid listingID);
    Task<List<ConversationRef>> GetConversationsAsync(Guid userID);
}