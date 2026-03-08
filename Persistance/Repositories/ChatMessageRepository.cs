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

    public async Task<List<ChatMessage>> GetAllMessagesAsync(Guid senderID, Guid receiverID, Guid listingID)
    {
        return await _context.ChatMessages
            .Where(c => c.listingID == listingID && c.senderID == senderID && c.receiverID == receiverID)
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
}