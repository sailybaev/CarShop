using CarShopFinal.Application.Features.Listing.ApproveListing;
using CarShopFinal.Application.Features.Listing.CreateListing;
using CarShopFinal.Application.Features.Listing.DeleteListing;
using CarShopFinal.Application.Features.Listing.GetListings;
using CarShopFinal.Application.Features.Listing.RejectListing;
using CarShopFinal.Application.Features.Listing.UpdateListing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;

[ApiController]
[Route("api/listing")]
public class ListingController : ControllerBase
{
    private readonly CreateListingHandler _createListingHandler;
    private readonly GetListingsHandler _getListingsHandler;
    private readonly ApproveListingHandler _approveListingHandler;
    private readonly RejectListingHandler _rejectListingHandler;
    private readonly DeleteListingHandler _deleteListingHandler;
    private readonly UpdateListingHandler _updateListingHandler;

    public ListingController(
        CreateListingHandler createListingHandler,
        GetListingsHandler getListingsHandler,
        ApproveListingHandler approveListingHandler,
        RejectListingHandler rejectListingHandler,
        DeleteListingHandler deleteListingHandler,
        UpdateListingHandler updateListingHandler)
    {
        _createListingHandler = createListingHandler;
        _getListingsHandler = getListingsHandler;
        _approveListingHandler = approveListingHandler;
        _rejectListingHandler = rejectListingHandler;
        _deleteListingHandler = deleteListingHandler;
        _updateListingHandler = updateListingHandler;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateListing([FromBody] CreateListingCommand command)
    {
        var id = await _createListingHandler.Handle(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetListings(
        [FromQuery] string? city,
        [FromQuery] string? brand,
        [FromQuery] string? model,
        [FromQuery] string? year,
        [FromQuery] decimal? priceFrom,
        [FromQuery] decimal? priceTo,
        [FromQuery] string? listingStatus,
        [FromQuery] string? moderationStatus)
    {
        var listings = await _getListingsHandler.Handle(
            new GetListingsQuery(city, brand, model, year, priceFrom, priceTo, listingStatus, moderationStatus));
        return Ok(listings);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateListing(Guid id, [FromBody] UpdateListingRequest request)
    {
        await _updateListingHandler.Handle(new UpdateListingCommand(id, request.City, request.Description));
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteListing(Guid id)
    {
        await _deleteListingHandler.Handle(new DeleteListingCommand(id));
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:guid}/approve")]
    public async Task<IActionResult> ApproveListing(Guid id)
    {
        await _approveListingHandler.Handle(new ApproveListingCommand(id));
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:guid}/reject")]
    public async Task<IActionResult> RejectListing(Guid id)
    {
        await _rejectListingHandler.Handle(new RejectListingCommand(id));
        return NoContent();
    }
}

public record UpdateListingRequest(string City, string Description);

//PRAVKA
/*
   private readonly GetListingsHandler _getListingsHandler;
   private readonly ApproveListingHandler _approveListingHandler;
   private readonly RejectListingHandler _rejectListingHandler;
   private readonly DeleteListingHandler _deleteListingHandler;
   private readonly UpdateListingHandler _updateListingHandler;
*/
