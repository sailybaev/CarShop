using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;
using CarShopFinal.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Persistance.Repositories;

public class ChatMessageRepository:IChatMessageRepository
{
    private readonly CarDbContext _context;
    public ChatMessageRepository(CarDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveMessageAsync(ChatMessage message)
    {
        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ChatMessage>> GetAllMessagesAsync(Guid userID, Guid otherUserID, Guid listingID)
    {
        return await _context.ChatMessages
            .Where(c => c.listingID == listingID &&
                ((c.senderID == userID && c.receiverID == otherUserID) ||
                 (c.senderID == otherUserID && c.receiverID == userID)))
            .OrderBy(z => z.CreatedAt).ToListAsync();
    }

    public async Task MarkAsReadAsync(Guid senderID, Guid receiverID, Guid listingID)
    {
        var messages= await _context.ChatMessages
            .Where(c => c.listingID == listingID && c.senderID == senderID && c.receiverID == receiverID && c.isRead == false)
            .OrderBy(z => z.CreatedAt).ToListAsync();
        foreach (var message in messages)
        {
            message.MarkAsRead();
        }
        await _context.SaveChangesAsync();
    }

    public async Task<List<ConversationRef>> GetConversationsAsync(Guid userID)
    {
        var sent = await _context.ChatMessages
            .Where(m => m.senderID == userID)
            .Select(m => new { m.listingID, other = m.receiverID })
            .Distinct()
            .ToListAsync();

        var received = await _context.ChatMessages
            .Where(m => m.receiverID == userID)
            .Select(m => new { m.listingID, other = m.senderID })
            .Distinct()
            .ToListAsync();

        return sent.Concat(received)
            .DistinctBy(x => (x.listingID, x.other))
            .Select(x => new ConversationRef(x.listingID, x.other))
            .ToList();
    }
}