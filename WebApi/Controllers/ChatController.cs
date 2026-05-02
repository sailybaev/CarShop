using System.Security.Claims;
using CarShopFinal.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class ChatController:ControllerBase
{
    private readonly IChatMessageRepository _chatMessageRepository;
    public ChatController(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    [HttpGet("conversations")]
    public async Task<IActionResult> GetConversations()
    {
        var me = GetMe();
        var conversations = await _chatMessageRepository.GetConversationsAsync(me);
        return Ok(conversations);
    }

    [HttpGet("history/{listingId:guid}/{userId:guid}")]
    public async Task<IActionResult> GetHistory(Guid listingId, Guid userId)
    {
        var me = GetMe();
        var messages = await _chatMessageRepository.GetAllMessagesAsync(me, userId, listingId);
        return Ok(messages);
    }

    private Guid GetMe()
    {
        var claim = User?.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null)
            throw new UnauthorizedAccessException("User is not authenticatate");
        return Guid.Parse(claim.Value);
    }
}